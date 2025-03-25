using Shopi.Product.Domain.Entities;
using Shopi.Product.Domain.Queries;

namespace Shopi.Product.Domain.Interfaces;

public interface IProductReadRepository
{
    Task<AppProduct?> Get(Guid id);
    Task<AppProduct?> GetBySku(string sku);
    Task<IReadOnlyCollection<AppProduct>> FilterProducts(ProductsQuery query);
    Task<int> GetCount(ProductsQuery query);
}