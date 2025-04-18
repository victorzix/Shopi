﻿using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs.Responses;
using Shopi.Product.Application.Queries.CategoriesQueries;
using Shopi.Product.Domain.Interfaces;

namespace Shopi.Product.API.QueryHandlers.CategoriesQueryHandlers;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ApiResponses<CategoryResponseDto>>
{
    private ICategoryReadRepository _readRepository;
    private IMapper _mapper;

    public GetCategoryQueryHandler(ICategoryReadRepository readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<CategoryResponseDto>> Handle(GetCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _readRepository.Get(request.Id);
        if (category == null)
        {
            throw new CustomApiException("Erro ao realizar operação", StatusCodes.Status404NotFound,
                "Categoria não encontrada");
        }

        return new ApiResponses<CategoryResponseDto>
        {
            Data = _mapper.Map<CategoryResponseDto>(category),
            Success = true,
        };
    }
}