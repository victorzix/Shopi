﻿namespace Shopi.Product.Domain.Queries;

public class ProductsQuery
{
    public List<string?>? CategoryIds { get; set; }
    public string? Name { get; set; }
    public string? Sku { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public bool Visible { get; set; } = true;
    public string Manufacturer { get; set; }
    public string? NameOrder { get; set; }
    public string? PriceOrder { get; set; }
    public string? ReviewOrder { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }

    public ProductsQuery()
    {
    }

    public ProductsQuery(List<string?>? categoryIds, string? name, string? sku, int? minPrice, int? maxPrice, bool visible, string manufacturer, string? nameOrder, string? priceOrder, string? reviewOrder, int limit, int offset)
    {
        CategoryIds = categoryIds;
        Name = name;
        Sku = sku;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        Visible = visible;
        Manufacturer = manufacturer;
        NameOrder = nameOrder;
        PriceOrder = priceOrder;
        ReviewOrder = reviewOrder;
        Limit = limit;
        Offset = offset;
    }
}