using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.Commands;

public class CreateProductCommand : IRequest<ApiResponses<CreateProductResponseDto>>
{
    public string Name { get; set; }
    public string? Sku { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Manufacturer { get; set; }

    public List<Guid>? CategoriesIds { get; set; }

    public CreateProductCommand()
    {
    }

    public CreateProductCommand(string name, string? sku, int price, int quantity, string description,
        string manufacturer, List<Guid>? categoriesIds)
    {
        Name = name;
        Sku = sku;
        Price = price;
        Quantity = quantity;
        Description = description;
        Manufacturer = manufacturer;
        CategoriesIds = categoriesIds;
    }
}