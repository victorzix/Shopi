using Shopi.Product.Domain.Entities;
using Shopi.Product.Domain.Queries;

namespace Shopi.Product.Domain.Interfaces;

public interface ICategoryReadRepository
{
    Task<Category?> Get(Guid id);
    Task<IReadOnlyCollection<Category>> FilterCategories(CategoriesQuery query);
    Task<List<Category>> GetMany(List<Guid> categoryIds);
    Task<int> GetCount(CategoriesQuery query);
}