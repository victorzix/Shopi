using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.Queries;

public class FilterCategoriesQuery : IRequest<ApiResponses<IReadOnlyCollection<Category>>>
{
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
    public bool Visible { get; set; } = true;

    public FilterCategoriesQuery()
    {
    }

    public FilterCategoriesQuery(string? name, Guid? parentId, bool visible)
    {
        Name = name;
        ParentId = parentId;
        Visible = visible;
    }
}