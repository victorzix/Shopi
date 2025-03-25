using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs.Responses;

namespace Shopi.Product.Application.Queries.ProductsQueries;

public class GetProductQuery : IRequest<ApiResponses<ProductResponseDto>>
{
    public Guid Id { get; set; }

    public GetProductQuery()
    {
    }

    public GetProductQuery(Guid id)
    {
        Id = id;
    }
}