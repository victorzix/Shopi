using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Domain.Entities;

namespace Shopi.Product.Application.Commands;

public class UpdateProductCommand : IRequest<ApiResponses<AppProduct>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Sku { get; set; }
    public int? Price { get; set; }
    public int? Quantity { get; set; }
    public string? Description { get; set; }
    public string? Manufacturer { get; set; }
}