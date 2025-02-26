using MediatR;
using Shopi.Core.Utils;
using Shopi.Customer.API.DTOs;

namespace Shopi.Customer.API.Commands;

public class UpdateCustomerCommand : IRequest<ApiResponses<CreateCustomerResponseDto>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public UpdateCustomerCommand(Guid id, string? name, string? email, string? password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }
}