using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs.Responses;

namespace Shopi.Product.Application.Queries.CategoriesQueries;

public class GetCategoryQuery : IRequest<ApiResponses<CategoryResponseDto>>
{
    public Guid Id { get; set; }

    public GetCategoryQuery()
    {
    }

    public GetCategoryQuery(Guid id)
    {
        Id = id;
    }
}