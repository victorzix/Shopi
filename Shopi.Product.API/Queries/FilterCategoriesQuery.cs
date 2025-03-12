using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.Queries;

public class FilterCategoriesQuery : IRequest<ApiResponses<FilterCategoriesResponseDto>>
{
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
    public bool Visible { get; set; } = true;
    public string? NameOrder { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }

    public FilterCategoriesQuery()
    {
    }

    public FilterCategoriesQuery(string? name, Guid? parentId, bool visible, string? nameOrder, int limit, int offset)
    {
        Name = name;
        ParentId = parentId;
        Visible = visible;
        NameOrder = nameOrder;
        Limit = limit;
        Offset = offset;
    }
}