using MediatR;
using Shopi.Core.Utils;
using Shopi.Customer.API.Commands;
using Shopi.Customer.API.DTOs;

namespace Shopi.Customer.API.CommandHandlers;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ApiResponses<CreateAddressResponse>>
{
    
    
    // public Task<ApiResponses<CreateAddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    // {
    // }
}