using MediatR;
using Shopi.Core.Utils;
using Shopi.Identity.Application.DTOs;

namespace Shopi.Identity.Application.Commands;

public class ConfirmEmailCommand : IRequest<ApiResponses<LoginUserResponseDto>>
{
    public string Token { get; set; }
    public ConfirmEmailCommand(string token)
    {
        Token = token;
    }
}