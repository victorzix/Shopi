using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Interfaces;
using Shopi.Product.API.Queries;

namespace Shopi.Product.API.QueryHandlers;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ApiResponses<CreateCategoryResponseDto>>
{
    private ICategoryReadRepository _readRepository;
    private IMapper _mapper;

    public GetCategoryQueryHandler(ICategoryReadRepository readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<CreateCategoryResponseDto>> Handle(GetCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _readRepository.Get(request.Id);
        if (category == null)
        {
            throw new CustomApiException("Erro ao realizar operação", StatusCodes.Status404NotFound,
                "Categoria não encontrada");
        }

        return new ApiResponses<CreateCategoryResponseDto>
        {
            Data = _mapper.Map<CreateCategoryResponseDto>(category),
            Success = true,
        };
    }
}