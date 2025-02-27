using Microsoft.EntityFrameworkCore;
using Shopi.Product.API.Data;
using Shopi.Product.API.Interfaces;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.Repositories;

public class ProductWriteRepository : IProductWriteRepository
{
    private readonly AppProductDbContext _dbContext;

    public ProductWriteRepository(AppProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppProduct> Create(AppProduct appProduct)
    {
        var product = await _dbContext.AppProducts.AddAsync(appProduct);
        await _dbContext.SaveChangesAsync();
        return product.Entity;
    }

    public async Task<AppProduct> Update(AppProduct appProduct)
    {
        var product = _dbContext.AppProducts.Update(appProduct);
        await _dbContext.SaveChangesAsync();
        return product.Entity;
    }

    public async Task ChangeVisibility(AppProduct appProduct)
    {
        await _dbContext.AppProducts
            .Where(p => p.Id == appProduct.Id)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(p => p.Visible, p => !p.Visible)
            );
    }

    public async Task Delete(AppProduct appProduct)
    {
        _dbContext.AppProducts.Remove(appProduct);
        await _dbContext.SaveChangesAsync();
    }
}