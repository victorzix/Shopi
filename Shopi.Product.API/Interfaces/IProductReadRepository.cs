using Shopi.Product.API.DTOs;
using Shopi.Product.API.Models;
using Shopi.Product.API.Queries;

namespace Shopi.Product.API.Interfaces;

public interface IProductReadRepository
{
    Task<AppProduct?> Get(Guid id);
    Task<AppProduct?> GetBySku(string sku);
    Task<IReadOnlyCollection<AppProduct>> FilterProducts(FilterProductsQuery query);
}