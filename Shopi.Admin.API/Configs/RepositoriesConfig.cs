// using Shopi.Admin.API.Interfaces;
// using Shopi.Admin.API.Repositories;

using Shopi.Admin.API.Interfaces;
using Shopi.Admin.API.Repositories;

namespace Shopi.Admin.API.Configs;

public static class RepositoriesConfig
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAdminReadRepository, AdminReadRepository>();
        services.AddScoped<IAdminWriteRepository, AdminWriteRepository>();
    }
}