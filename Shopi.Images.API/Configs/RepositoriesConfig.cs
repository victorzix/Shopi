using Shopi.Images.Domain.Interfaces;
using Shopi.Images.Infrastructure.Repositories;
using Shopi.Images.API.Services;
using Shopi.Images.Application.Interfaces;

namespace Shopi.Images.API.Configs;

public static class RepositoriesConfig
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IImageReadRepository, ImageReadRepository>();
        services.AddScoped<IImageWriteRepository, ImageWriteRepository>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
    }
}