using Shopi.Product.API.Models;

namespace Shopi.Product.API.DTOs;

public class FilterCategoriesResponseDto
{
    public IReadOnlyCollection<Category>? Categories { get; set; }
    public int Total { get; set; }
}