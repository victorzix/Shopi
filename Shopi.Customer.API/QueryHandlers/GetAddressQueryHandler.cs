using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Customer.API.DTOs;
using Shopi.Customer.API.Interfaces;
using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;

namespace Shopi.Customer.API.QueryHandlers;

public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, ApiResponses<Address>>
{
    private IAddressReadRepository _repository;
    private ICustomerReadRepository _customerRepository;

    public GetAddressQueryHandler(IAddressReadRepository repository, ICustomerReadRepository customerRepository)
    {
        _repository = repository;
        _customerRepository = customerRepository;
    }

    public async Task<ApiResponses<Address>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FilterClient(new FilterCustomerQuery { Id = request.CustomerId });
        if (customer == null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status404NotFound, "Usuário não encontrado");
        }

        var address = await _repository.Get(new GetAddressQuery(request.Id, request.CustomerId));

        return new ApiResponses<Address> { Data = address, Success = true };
    }
}