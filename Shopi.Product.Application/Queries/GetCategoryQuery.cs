using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs;

namespace Shopi.Product.Application.Queries;

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