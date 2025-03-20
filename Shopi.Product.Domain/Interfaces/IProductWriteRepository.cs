using Shopi.Product.Domain.Entities;

namespace Shopi.Product.Domain.Interfaces;

public interface IProductWriteRepository
{
    Task<AppProduct> Create(AppProduct appProduct);
    Task<AppProduct> Update(AppProduct appProduct);
    Task ChangeVisibility(AppProduct appProduct);
    Task Delete(AppProduct appProduct);
}