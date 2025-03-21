using MediatR;
using Shopi.Core.Utils;
using Shopi.Customer.Application.DTOs;

namespace Shopi.Customer.Application.Commands;

public class UpdateAddressCommand : IRequest<ApiResponses<CreateAddressResponse>>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string? Title { get; set; }
    public string? Street { get; set; }
    public string? District { get; set; }
    public int? Number { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Complement { get; set; }

    public UpdateAddressCommand()
    {
    }

    public UpdateAddressCommand(Guid id, Guid customerId, string? title, string? street, string? district, int? number,
        string? zipCode, string? city, string? state, string? complement)
    {
        Id = id;
        CustomerId = customerId;
        Title = title;
        Street = street;
        District = district;
        Number = number;
        ZipCode = zipCode;
        City = city;
        State = state;
        Complement = complement;
    }
}