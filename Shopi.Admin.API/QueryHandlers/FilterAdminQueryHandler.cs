using AutoMapper;
using MediatR;
using Shopi.Admin.Application.Queries;
using Shopi.Admin.Domain.Entities;
using Shopi.Admin.Domain.Interfaces;
using Shopi.Admin.Domain.Queries;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;

namespace Shopi.Admin.API.QueryHandlers;

public class FilterAdminQueryHandler : IRequestHandler<FilterAdminQuery, ApiResponses<AppAdmin>>
{
    private readonly IAdminReadRepository _readRepository;
    private readonly IMapper _mapper;

    public FilterAdminQueryHandler(IMapper mapper, IAdminReadRepository readRepository)
    {
        _mapper = mapper;
        _readRepository = readRepository;
    }

    public async Task<ApiResponses<AppAdmin>> Handle(FilterAdminQuery request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map<QueryAdmin>(request);
        var customer = await _readRepository.FilterAdmin(query);
        if (customer == null)
        {
            throw new CustomApiException("Erro de pesquisa", StatusCodes.Status404NotFound,
                "Administrador não encontrado");
        }

        return new ApiResponses<AppAdmin>
            { Success = true, Data = customer };
    }
}