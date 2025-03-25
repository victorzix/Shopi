using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs.Responses;
using Shopi.Product.Application.Queries.ProductsQueries;
using Shopi.Product.Domain.Interfaces;

namespace Shopi.Product.API.QueryHandlers.ProductsQueryHandlers;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ApiResponses<ProductResponseDto>>
{
    private readonly IProductReadRepository _readRepository;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IProductReadRepository readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<ProductResponseDto>> Handle(GetProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _readRepository.Get(request.Id);
        if (product == null)
        {
            throw new CustomApiException("Erro ao realizar operação", StatusCodes.Status404NotFound,
                "Produto não encontrado");
        }

        return new ApiResponses<ProductResponseDto>
        {
            Data = _mapper.Map<ProductResponseDto>(product),
            Success = true,
        };
    }
}