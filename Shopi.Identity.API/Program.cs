using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Shopi.Core.Exceptions;
using Shopi.Core.Interfaces;
using Shopi.Core.Services;
using Shopi.Identity.API.Configs;
using Shopi.Identity.API.Services;
using Shopi.Identity.Infrastructure.Data;
using Shopi.Identity.Infrastructure.Interfaces;
using Shopi.Identity.Infrastructure.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IIdentityJwtService, IdentityJwtService>();
builder.Services.AddScoped<IBffHttpClient, BffHttpClient>();

builder.Services.AddHttpClient<BffHttpClient>();
builder.Services.AddScoped<IdentityJwtService>();
builder.Services.AddAutoMapper(typeof(UserMappingProfile));

builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins(["http://shopi.bff:8080", "http://shopi.customer.api:8081"])
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

await IdentityConfig.InitializeRoles(app);

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


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();