using Shopi.Product.Domain.Entities;

namespace Shopi.Product.Application.DTOs.Responses;

public class FilterCategoriesResponseDto
{
    public IReadOnlyCollection<Category>? Categories { get; set; }
    public int Total { get; set; }
}