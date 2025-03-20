using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs;

namespace Shopi.Product.Application.Commands;

public class UpdateCategoryCommand : IRequest<ApiResponses<CreateCategoryResponseDto>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }

    public UpdateCategoryCommand()
    {
    }

    public UpdateCategoryCommand(Guid id, string? name, Guid? parentId, string? description)
    {
        Id = id;
        Name = name;
        ParentId = parentId;
        Description = description;
    }
}