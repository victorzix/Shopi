using AutoMapper;
using MediatR;
using Shopi.Admin.API.Interfaces;
using Shopi.Admin.API.Models;
using Shopi.Admin.API.Queries;
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
        var customer = await _readRepository.FilterAdmin(request);
        if (customer == null)
        {
            throw new CustomApiException("Erro de pesquisa", StatusCodes.Status404NotFound,
                "Administrador não encontrado");
        }

        return new ApiResponses<AppAdmin>
            { Success = true, Data = customer };
    }
}