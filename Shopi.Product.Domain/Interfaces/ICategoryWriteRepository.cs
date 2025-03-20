using Shopi.Product.Domain.Entities;

namespace Shopi.Product.Domain.Interfaces;

public interface ICategoryWriteRepository
{
    Task<Category> Create(Category category);
    Task<Category> Update(Category category);
    Task ChangeVisibility(Category category);
    Task Deactivate(Category category);
}