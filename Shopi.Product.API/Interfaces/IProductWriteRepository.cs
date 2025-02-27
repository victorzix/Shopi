using Shopi.Product.API.Models;

namespace Shopi.Product.API.Interfaces;

public interface IProductWriteRepository
{
    Task<AppProduct> Create(AppProduct appProduct);
    Task<AppProduct> Update(AppProduct appProduct);
    Task ChangeVisibility(AppProduct appProduct);
    Task Delete(AppProduct appProduct);
}