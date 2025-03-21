using Shopi.Admin.Domain.Interfaces;
using Shopi.Admin.Infrastructure.Repositories;

namespace Shopi.Admin.API.Configs;

public static class RepositoriesConfig
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAdminReadRepository, AdminReadRepository>();
        services.AddScoped<IAdminWriteRepository, AdminWriteRepository>();
    }
}