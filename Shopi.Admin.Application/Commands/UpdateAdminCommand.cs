using MediatR;
using Shopi.Admin.Application.DTOs;
using Shopi.Core.Utils;

namespace Shopi.Admin.Application.Commands;

public class UpdateAdminCommand : IRequest<ApiResponses<CreateAdminResponseDto>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public UpdateAdminCommand(Guid id, string? name, string? email, string? password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }
}