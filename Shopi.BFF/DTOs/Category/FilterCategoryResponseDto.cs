namespace Shopi.BFF.DTOs.Category;

public class FilterCategoryResponseDto
{
    public IReadOnlyCollection<Category>? Categories { get; set; }
    public int Total { get; set; }
}