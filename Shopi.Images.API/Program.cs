using Microsoft.AspNetCore.Diagnostics;
using Shopi.Core.Exceptions;
using Shopi.Core.Interfaces;
using Shopi.Core.Services;
using Shopi.Images.API.Configs;
using Shopi.Images.API.Data;
using Shopi.Images.API.Mappers;
using Shopi.Images.API.Middlewares;
using Shopi.Images.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<CloudinaryService>();
builder.Services.AddScoped<IBffHttpClient, BffHttpClient>();
builder.Services.AddHttpClient<BffHttpClient>();

builder.Services.AddAutoMapper(typeof(ImageMappingProfile));
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

app.MapControllers();

app.Run();