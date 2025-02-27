using Shopi.Product.API.Models;

namespace Shopi.Product.API.Interfaces;

public interface IProductCategoryWriteRepository
{
    Task AssociateCategoryToProduct(AppProduct product, List<Category> categories);
    Task RemoveCategoryFromProduct(Guid productId, List<Guid> categoryIds);
}