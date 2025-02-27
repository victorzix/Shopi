using Shopi.Product.API.Models;

namespace Shopi.Product.API.Interfaces;

public interface ICategoryWriteRepository
{
    Task<Category> Create(Category category);
    Task<Category> Update(Category category);
    Task ChangeVisibility(Category category);
}