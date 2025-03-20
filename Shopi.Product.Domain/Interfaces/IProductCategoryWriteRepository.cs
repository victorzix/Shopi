using Shopi.Product.Domain.Entities;

namespace Shopi.Product.Domain.Interfaces;

public interface IProductCategoryWriteRepository
{
    Task AssociateCategoryToProduct(AppProduct product, List<Category> categories);
    Task RemoveCategoryFromProduct(Guid productId, List<Guid> categoryIds);
}