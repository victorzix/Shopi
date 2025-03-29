using Microsoft.EntityFrameworkCore;
using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Domain.Interfaces;
using Shopi.Customer.Infrastructure.Data;

namespace Shopi.Customer.Infrastructure.Repository;

public class CustomerWriteRepository : ICustomerWriteRepository
{
    private readonly AppCustomerDbContext _dbContext;

    public CustomerWriteRepository(AppCustomerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppCustomer> Create(AppCustomer appCustomerData)
    {
        var customer = await _dbContext.AddAsync(appCustomerData);
        await _dbContext.SaveChangesAsync();
        return customer.Entity;
    }

    public async Task<AppCustomer?> Update(AppCustomer customerData)
    {
        var customer = _dbContext.AppCustomer.Update(customerData);
        await _dbContext.SaveChangesAsync();
        return customer.Entity;
    }

    public async Task Delete(AppCustomer customer)
    {
        await _dbContext.AppCustomer.Where(c => c.Id == customer.Id).ExecuteUpdateAsync(setters =>
            setters.SetProperty(c => c.IsActive, false));
        await _dbContext.SaveChangesAsync();
    }
}