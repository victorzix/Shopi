using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Customer.Domain.Interfaces;
using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Application.Queries;
using Shopi.Customer.Domain.Queries;

namespace Shopi.Customer.API.QueryHandlers;

public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, ApiResponses<Address>>
{
    private IAddressReadRepository _repository;
    private ICustomerReadRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAddressQueryHandler(IAddressReadRepository repository, ICustomerReadRepository customerRepository,
        IMapper mapper)
    {
        _repository = repository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<Address>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        var customerQuery = _mapper.Map<QueryCustomer>(new FilterCustomerQuery { Id = request.CustomerId });
        var customer = await _customerRepository.FilterClient(customerQuery);
        if (customer == null)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status404NotFound, "Usuário não encontrado");
        }

        var addressQuery = _mapper.Map<QueryAddress>(new GetAddressQuery(request.Id, request.CustomerId));

        var address = await _repository.Get(addressQuery);

        return new ApiResponses<Address> { Data = address, Success = true };
    }
}