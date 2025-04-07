namespace Shopi.Identity.Domain.Interfaces;

public interface IEmailStrategy
{
    Task SendMessageAsync(string to, string body);
}