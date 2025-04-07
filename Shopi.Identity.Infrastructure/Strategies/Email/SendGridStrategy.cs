using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shopi.Identity.Domain.Interfaces;

namespace Shopi.Identity.Infrastructure.Strategies.Email;

public class SendGridStrategy : IEmailStrategy
{
    private readonly SendGridClient _client;

    public SendGridStrategy(IConfiguration configuration)
    {
        var apiKey = configuration.GetValue<string>("SendGridApiKey");
        _client = new SendGridClient(apiKey);
    }

    public async Task SendMessageAsync(string to, string body)
    {
        var from = new EmailAddress("viphaelnev@gmail.com");
        var destination = new EmailAddress(to);
        var msg = MailHelper.CreateSingleTemplateEmail(from, destination,
            "d-2d2b031c90ee40fd8209968d10dfdbe7", new { button_url = $"http://localhost:4200/confirm-email?token={body}" });
        await _client.SendEmailAsync(msg).ConfigureAwait(false);
    }
}