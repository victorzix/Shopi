using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.Commands;

public class CreateCategoryCommand : IRequest<ApiResponses<CreateCategoryResponseDto>>
{
    public string Name { get; set; } 
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }

    public CreateCategoryCommand(string name, Guid? parentId, string? description)
    {
        Name = name;
        ParentId = parentId;
        Description = description;
    }
}