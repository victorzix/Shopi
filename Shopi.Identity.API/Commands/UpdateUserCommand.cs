using MediatR;
using Shopi.Core.Utils;

namespace Shopi.Identity.API.Commands;

public class UpdateUserCommand : IRequest<ApiResponses<string>>
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public UpdateUserCommand(Guid id, string? email, string? password)
    {
        Id = id;
        Email = email;
        Password = password;
    }
}