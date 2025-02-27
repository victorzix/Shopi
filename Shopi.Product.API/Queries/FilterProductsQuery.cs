using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.Queries;

public class FilterProductsQuery : IRequest<ApiResponses<IReadOnlyCollection<AppProduct?>>>
{
    public List<string?>? CategoryIds { get; set; }
    public string? Name { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public bool Visible { get; set; } = true;
    public string Manufacturer { get; set; }
    public string? NameOrder { get; set; }
    public string? PriceOrder { get; set; }
    public string? ReviewOrder { get; set; }
    public PaginationFilterDto PaginationFilter { get; set; }

    public FilterProductsQuery(PaginationFilterDto paginationFilter)
    {
        PaginationFilter = paginationFilter;
    }

    public FilterProductsQuery(List<string?>? categoryIds, string? name, int? minPrice, int? maxPrice, bool visible,
        string manufacturer, string? nameOrder, string? priceOrder, string? reviewOrder,
        PaginationFilterDto paginationFilter)
    {
        CategoryIds = categoryIds;
        Name = name;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        Visible = visible;
        Manufacturer = manufacturer;
        NameOrder = nameOrder;
        PriceOrder = priceOrder;
        ReviewOrder = reviewOrder;
        PaginationFilter = paginationFilter;
    }
}