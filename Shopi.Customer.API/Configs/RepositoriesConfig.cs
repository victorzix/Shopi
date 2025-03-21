using Shopi.Customer.Domain.Interfaces;
using Shopi.Customer.Infrastructure.Repository;

namespace Shopi.Customer.API.Configs;

public static class RepositoriesConfig
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();

        services.AddScoped<IAddressReadRepository, AddressReadRepository>();
        services.AddScoped<IAddressWriteRepository, AddressWriteRepository>();
    }
}