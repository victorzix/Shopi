using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Customer.API.Interfaces;
using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;

namespace Shopi.Customer.API.QueryHandlers;

public class GetByEmailOrDocumentQueryHandler : IRequestHandler<GetByEmailOrDocumentQuery, ApiResponses<AppCustomer>>
{
    private readonly ICustomerReadRepository _readRepository;
    private readonly IMapper _mapper;

    public GetByEmailOrDocumentQueryHandler(IMapper mapper, ICustomerReadRepository readRepository)
    {
        _mapper = mapper;
        _readRepository = readRepository;
    }

    public async Task<ApiResponses<AppCustomer>> Handle(GetByEmailOrDocumentQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _readRepository.GetByEmailOrDocument(request);
        if (customer == null)
        {
            throw new CustomApiException("Erro de pesquisa", StatusCodes.Status404NotFound, "Cliente não encontrado");
        }
        
        return new ApiResponses<AppCustomer>
            { Success = true, Data = customer };
    }
}