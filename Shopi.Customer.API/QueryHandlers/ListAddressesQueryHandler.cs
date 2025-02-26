using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Customer.API.Interfaces;
using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;

namespace Shopi.Customer.API.QueryHandlers;

public class ListAddressesQueryHandler : IRequestHandler<ListAddressesQuery, ApiResponses<IEnumerable<Address?>>>
{
    private IAddressReadRepository _repository;
    private ICustomerReadRepository _customerRepository;

    public ListAddressesQueryHandler(IAddressReadRepository repository, ICustomerReadRepository customerRepository)
    {
        _repository = repository;
        _customerRepository = customerRepository;
    }

    public async Task<ApiResponses<IEnumerable<Address?>>> Handle(ListAddressesQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FilterClient(new FilterCustomerQuery { Id = request.CustomerId });
        if (customer == null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status404NotFound, "Usuário não encontrado");
        }

        request.CustomerId = customer.Id;
        
        return new ApiResponses<IEnumerable<Address?>> { Data = await _repository.List(request), Success = true };
    }
}