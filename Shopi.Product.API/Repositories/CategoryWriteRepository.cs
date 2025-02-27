using Microsoft.EntityFrameworkCore;
using Shopi.Product.API.Data;
using Shopi.Product.API.Interfaces;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.Repositories;

public class CategoryWriteRepository : ICategoryWriteRepository
{
    private readonly AppProductDbContext _dbContext;

    public CategoryWriteRepository(AppProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> Create(Category dto)
    {
        var category = await _dbContext.Categories.AddAsync(dto);
        await _dbContext.SaveChangesAsync();
        return category.Entity;
    }

    public async Task<Category> Update(Category category)
    {
        var updatedCategory = _dbContext.Categories.Update(category);
        await _dbContext.SaveChangesAsync();
        return updatedCategory.Entity;
    }

    public async Task ChangeVisibility(Category category)
    {
        await _dbContext.Categories
            .Where(p => p.Id == category.Id)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(p => p.Visible, p => !p.Visible)
            );
    }
}