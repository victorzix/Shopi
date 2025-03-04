using MediatR;
using Shopi.Admin.API.DTOs;
using Shopi.Core.Utils;

namespace Shopi.Admin.API.Commands;

public class CreateAdminCommand : IRequest<ApiResponses<CreateAdminResponseDto>>
{
    public string Name { get; }
    public string Email { get; }
    public Guid UserId { get; }

    public CreateAdminCommand(string name, string email, Guid userId)
    {
        Name = name;
        Email = email;
        UserId = userId;
    }
}