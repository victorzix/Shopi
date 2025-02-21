using MediatR;
using Shopi.Core.Utils;
using Shopi.Identity.API.DTOs;

namespace Shopi.Identity.API.Commands;

public class LoginUserCommand : IRequest<ApiResponses<LoginUserResponseDto>>
{
    public string Email { get; }
    public string Password { get; }

    public LoginUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}