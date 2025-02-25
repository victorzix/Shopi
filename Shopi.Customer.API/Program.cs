using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Shopi.Core.Exceptions;
using Shopi.Core.Services;
using Shopi.Customer.API.Configs;
using Shopi.Customer.API.Data;
using Shopi.Customer.API.Interfaces;
using Shopi.Customer.API.Mappers;
using Shopi.Customer.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfigs();

builder.Services.AddHttpClient<BffHttpClient>();
builder.Services.AddAutoMapper(typeof(CustomerMappingProfile));

builder.Services.AddDbContext<AppCustomerDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ElevatedRights", policy => policy.RequireRole("Administrator"))
    .AddPolicy("CustomerRights", policy => policy.RequireRole("Customer", "Administrator"));

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();