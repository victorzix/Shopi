using AutoMapper;
using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs.Responses;
using Shopi.Product.Application.Queries.CategoriesQueries;
using Shopi.Product.Domain.Interfaces;
using Shopi.Product.Domain.Queries;

namespace Shopi.Product.API.QueryHandlers.CategoriesQueryHandlers;

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
        var query = _mapper.Map<CategoriesQuery>(request);
        var categories = await _readRepository.FilterCategories(query);
        var categoriesCount = await _readRepository.GetCount(query);
        var response = new FilterCategoriesResponseDto { Categories = categories, Total = categoriesCount };
        return new ApiResponses<FilterCategoriesResponseDto>
        {
            Data = response,
            Success = true
        };
    }
}