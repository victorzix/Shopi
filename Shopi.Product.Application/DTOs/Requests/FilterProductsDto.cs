namespace Shopi.Product.Application.DTOs.Requests;

public class FilterProductsDto
{
    public List<string?>? CategoryIds { get; set; }
    public string? Name { get; set; }
    public string? Sku { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public bool? Visible { get; set; }
    public string? Manufacturer { get; set; }
    public string? NameOrder { get; set; }
    public string? PriceOrder { get; set; }
    public string? ReviewOrder { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}