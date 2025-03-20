using Microsoft.EntityFrameworkCore;
using Shopi.Product.Infrastructure.Data;
using Shopi.Product.Domain.Interfaces;
using Shopi.Product.Domain.Entities;

namespace Shopi.Product.Infrastructure.Repositories;

public class ProductCategoryWriteRepository : IProductCategoryWriteRepository
{
    private readonly AppProductDbContext _dbContext;

    public ProductCategoryWriteRepository(AppProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AssociateCategoryToProduct(AppProduct product, List<Category> categories)
    {
        var associations = categories.Select(category => new AppProductCategory
        {
            ProductId = product.Id,
            CategoryId = category.Id
        }).ToList();

        await _dbContext.AppProductCategories.AddRangeAsync(associations);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveCategoryFromProduct(Guid productId, List<Guid> categoryIds)
    {
        var associations = await _dbContext.AppProductCategories
            .Where(pc => pc.ProductId == productId && categoryIds.Contains(pc.CategoryId))
            .ToListAsync();

        if (associations.Count == 0)
            throw new Exception("Produto e categoria não estão associados ou as categorias não foram encontradas");

        _dbContext.AppProductCategories.RemoveRange(associations);

        await _dbContext.SaveChangesAsync();
    }
}