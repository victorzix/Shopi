namespace Shopi.Core.Utils;

public static class MicroServicesUrls
{
    public static readonly Uri AuthApiUrl = new Uri("http://localhost:5143/auth/");
    public static readonly Uri CustomerApiUrl = new Uri("http://localhost:5118/customer/");
}