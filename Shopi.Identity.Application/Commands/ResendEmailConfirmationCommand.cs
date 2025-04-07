using MediatR;
using Shopi.Core.Utils;
using Shopi.Identity.Application.DTOs;

namespace Shopi.Identity.Application.Commands;

public class ResendEmailConfirmationCommand : IRequest
{
    public string Token { get; set; }
    public ResendEmailConfirmationCommand(string token)
    {
        Token = token;
    }
}