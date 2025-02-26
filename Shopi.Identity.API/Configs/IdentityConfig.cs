using Microsoft.AspNetCore.Identity;
using Shopi.Identity.API.Data;

namespace Shopi.Identity.API.Configs;

public static class IdentityConfig
{
    public static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();
        services.AddAuthorizationBuilder()
            .AddPolicy("ElevatedRights", policy => policy.RequireRole("Administrator"))
            .AddPolicy("CustomerRights", policy => policy.RequireRole("Customer", "Administrator"));
        
    }
    
    public static async Task InitializeRoles(IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { "Administrator", "Customer" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}