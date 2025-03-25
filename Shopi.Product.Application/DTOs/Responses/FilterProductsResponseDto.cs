using Shopi.Product.Domain.Entities;

namespace Shopi.Product.Application.DTOs.Responses;

public class FilterProductsResponseDto
{
    public IReadOnlyCollection<AppProduct>? Products { get; set; }
    public int Total { get; set; }
}