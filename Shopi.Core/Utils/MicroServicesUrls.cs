namespace Shopi.Core.Utils;

public static class MicroServicesUrls
{
    public static Uri AuthApiUrl { get; private set; }
    public static Uri CustomerApiUrl { get; private set; }

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
    }
}