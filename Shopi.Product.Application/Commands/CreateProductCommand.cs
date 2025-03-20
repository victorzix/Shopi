using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.Application.DTOs;

namespace Shopi.Product.Application.Commands;

public class CreateProductCommand : IRequest<ApiResponses<CreateProductResponseDto>>
{
    public string Name { get; set; }
    public string? Sku { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; } = 0;
    public string Description { get; set; }
    public string Manufacturer { get; set; }

    public CreateProductCommand()
    {
    }

    public CreateProductCommand(string name, string? sku, int price, int quantity, string description,
        string manufacturer)
    {
        Name = name;
        Sku = sku;
        Price = price;
        Quantity = quantity;
        Description = description;
        Manufacturer = manufacturer;
    }
}