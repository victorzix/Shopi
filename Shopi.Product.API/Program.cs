using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Shopi.Core.Exceptions;
using Shopi.Core.Interfaces;
using Shopi.Core.Services;
using Shopi.Product.API.Configs;
using Shopi.Product.API.Middlewares;
using Shopi.Product.Infrastructure.Data;
using Shopi.Product.Infrastructure.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerConfigs();

builder.Services.AddScoped<IBffHttpClient, BffHttpClient>();
builder.Services.AddHttpClient<BffHttpClient>();
builder.Services.AddAutoMapper(typeof(CategoryMappingProfile));
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));

builder.Services.AddDbContext<AppProductDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ElevatedRights", policy => policy.RequireRole("Administrator").RequireAuthenticatedUser())
    .AddPolicy("CustomerRights", policy => policy.RequireRole("Customer", "Administrator").RequireAuthenticatedUser());

builder.Services.AddRepositories();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins(["http://shopi.bff:8080", "http://shopi.auth.api:8082"])
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is CustomApiException apiException)
        {
            context.Response.StatusCode = apiException.StatusCode;
            context.Response.ContentType = "application/json";

            var problemDetails = apiException.ToProblemDetails();
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = "Erro interno no servidor" });
        }
    });
});

app.UseMiddleware<UnauthorizedMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();