namespace Shopi.Core.Utils;

public static class MicroServicesUrls
{
    public static Uri AuthApiUrl { get; private set; }
    public static Uri CustomerApiUrl { get; private set; }
    public static Uri AdminApiUrl { get; private set; }
    public static Uri ProductApiUrl { get; private set; }
    public static Uri CategoryApiUrl { get; private set; }

    static MicroServicesUrls()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("core.appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"core.appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        AuthApiUrl = new Uri(configuration["Microservices:AuthApi"] ?? "http://localhost:5143/auth/");
        CustomerApiUrl = new Uri(configuration["Microservices:CustomerApi"] ?? "http://localhost:5118/customer/");
        AdminApiUrl = new Uri(configuration["Microservices:AdminApi"] ?? "http://localhost:5129/admin/");
        ProductApiUrl = new Uri(configuration["Microservices:ProductApi"] ?? "http://localhost:5186/product/");
        CategoryApiUrl = new Uri(configuration["Microservices:CategoryApi"] ?? "http://localhost:5186/category/");
    }
}