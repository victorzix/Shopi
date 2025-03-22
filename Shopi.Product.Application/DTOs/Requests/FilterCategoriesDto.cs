namespace Shopi.Product.Application.DTOs.Requests;

public class FilterCategoriesDto
{
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
    public bool? Visible { get; set; }
    public string? NameOrder { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
}