using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs.Responses;


namespace Shopi.Product.Application.Commands.CategoriesCommands;

public class CreateCategoryCommand : IRequest<ApiResponses<CategoryResponseDto>>
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