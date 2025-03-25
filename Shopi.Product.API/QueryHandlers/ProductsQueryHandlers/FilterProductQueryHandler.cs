using AutoMapper;
using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs.Responses;
using Shopi.Product.Application.Queries.ProductsQueries;
using Shopi.Product.Domain.Interfaces;
using Shopi.Product.Domain.Queries;

namespace Shopi.Product.API.QueryHandlers.ProductsQueryHandlers;

public class FilterProductQueryHandler : IRequestHandler<FilterProductsQuery, ApiResponses<FilterProductsResponseDto>>
{
    private readonly IProductReadRepository _readRepository;
    private readonly IMapper _mapper;

    public FilterProductQueryHandler(IProductReadRepository readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<FilterProductsResponseDto>> Handle(FilterProductsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map<ProductsQuery>(request);
        var products = await _readRepository.FilterProducts(query);
        var productsCount = await _readRepository.GetCount(query);
        var response = new FilterProductsResponseDto { Products = products, Total = productsCount };
        return new ApiResponses<FilterProductsResponseDto>
        {
            Data = response,
            Success = true
        };
    }
}