using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.API.DTOs;

namespace Shopi.Product.API.Commands;

public class ChangeVisibilityCommand : IRequest<ApiResponses<CreateCategoryResponseDto>>
{
    public Guid Id { get; set; }

    public ChangeVisibilityCommand(Guid id)
    {
        Id = id;
    }
}