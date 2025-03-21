using MediatR;
using Shopi.Core.Utils;
using Shopi.Customer.Application.DTOs;

namespace Shopi.Customer.Application.Commands;

public class CreateCustomerCommand : IRequest<ApiResponses<CreateCustomerResponseDto>>
{
    public string Name { get; }
    public string Email { get; }
    public string Document { get; }
    public Guid UserId { get; }

    public CreateCustomerCommand(string name, string email, string document, Guid userId)
    {
        Name = name;
        Email = email;
        Document = document;
        UserId = userId;
    }
}