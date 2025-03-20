using Shopi.Images.API.Interfaces;
using Shopi.Images.API.Repositories;
using Shopi.Images.API.Services;

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