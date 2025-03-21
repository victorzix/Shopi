﻿using Shopi.Product.Domain.Interfaces;
using Shopi.Product.Infrastructure.Repositories;

namespace Shopi.Product.API.Configs;

public static class RepositoriesConfig
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
        
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        
        services.AddScoped<IProductCategoryWriteRepository, ProductCategoryWriteRepository>();
    }
}