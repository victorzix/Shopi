using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs.Responses;

namespace Shopi.Product.Application.Commands.ProductsCommands;

public class UpdateProductCommand : IRequest<ApiResponses<ProductResponseDto>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Sku { get; set; }
    public int? Price { get; set; }
    public int? Quantity { get; set; }
    public string? Description { get; set; }
    public string? Manufacturer { get; set; }
}