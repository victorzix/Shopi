namespace Shopi.Identity.Application.Interfaces;

public interface IEmailService
{
    Task SendMessageAsync(string to, string body);
}