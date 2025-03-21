using MediatR;
using Shopi.Core.Utils;
using Shopi.Identity.Application.DTOs;

namespace Shopi.Identity.Application.Commands;

public class CreateUserCommand : IRequest<ApiResponses<LoginUserResponseDto>>
{
    public string Name { get; }
    public string Email { get; }
    public string? Document { get; }
    public string Password { get; }
    public string Role { get; }

    public CreateUserCommand(string name, string email, string? document, string password, string role)
    {
        Name = name;
        Email = email;
        Document = document;
        Password = password;
        Role = role;
    }
}