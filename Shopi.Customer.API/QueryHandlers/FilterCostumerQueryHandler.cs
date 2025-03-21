using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Customer.Domain.Interfaces;
using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Application.Queries;
using Shopi.Customer.Domain.Queries;

namespace Shopi.Customer.API.QueryHandlers;

public class FilterCostumerQueryHandler : IRequestHandler<FilterCustomerQuery, ApiResponses<AppCustomer>>
{
    private readonly ICustomerReadRepository _readRepository;
    private readonly IMapper _mapper;

    public FilterCostumerQueryHandler(IMapper mapper, ICustomerReadRepository readRepository)
    {
        _mapper = mapper;
        _readRepository = readRepository; 
    }

    public async Task<ApiResponses<AppCustomer>> Handle(FilterCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map<QueryCustomer>(request);
        
        var customer = await _readRepository.FilterClient(query);
        if (customer == null)
        {
            throw new CustomApiException("Erro de pesquisa", StatusCodes.Status404NotFound, "Cliente não encontrado");
        }

        return new ApiResponses<AppCustomer>
            { Success = true, Data = customer };
    }
}