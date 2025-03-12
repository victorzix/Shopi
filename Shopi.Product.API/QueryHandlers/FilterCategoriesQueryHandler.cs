using AutoMapper;
using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Interfaces;
using Shopi.Product.API.Models;
using Shopi.Product.API.Queries;

namespace Shopi.Product.API.QueryHandlers;

public class
    FilterCategoriesQueryHandler : IRequestHandler<FilterCategoriesQuery, ApiResponses<FilterCategoriesResponseDto>>
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly IMapper _mapper;

    public FilterCategoriesQueryHandler(ICategoryReadRepository readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<FilterCategoriesResponseDto>> Handle(FilterCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _readRepository.FilterCategories(request);
        var categoriesCount = await _readRepository.GetCount(request);
        var response = new FilterCategoriesResponseDto { Categories = categories, Total = categoriesCount };
        return new ApiResponses<FilterCategoriesResponseDto>
        {
            Data = response,
            Success = true
        };
    }
}