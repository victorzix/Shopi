using Shopi.Identity.Application.Interfaces;
using Shopi.Identity.Domain.Interfaces;

namespace Shopi.Identity.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IEmailStrategy _emailStrategy;

    public EmailService(IEmailStrategy emailStrategy)
    {
        _emailStrategy = emailStrategy;
    }

    public async Task SendMessageAsync(string to, string body)
    {
        await _emailStrategy.SendMessageAsync(to, body);
    }
}