using Microsoft.EntityFrameworkCore;
using Shopi.Product.Domain.Interfaces;
using Shopi.Product.Domain.Entities;
using Shopi.Product.Infrastructure.Data;

namespace Shopi.Product.Infrastructure.Repositories;

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
        await _dbContext.SaveChangesAsync();
    }

    public async Task Deactivate(Category category)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        await _dbContext.Categories
            .Where(c => c.Id == category.Id || c.ParentId == category.Id)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(c => c.IsActive, false));

        await transaction.CommitAsync();
    }
}