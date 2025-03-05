using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.API.DTOs;

namespace Shopi.Product.API.Queries;

public class GetCategoryQuery : IRequest<ApiResponses<CreateCategoryResponseDto>>
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