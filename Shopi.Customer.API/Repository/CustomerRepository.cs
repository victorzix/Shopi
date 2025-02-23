using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shopi.Customer.API.Queries;
using Shopi.Customer.API.Data;
using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Repository;

public class CustomerRepository
{
    private readonly AppCustomerDbContext _dbContext;

    public CustomerRepository(AppCustomerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppCustomer> Create(AppCustomer appCustomerData)
    {
        var customer = await _dbContext.AddAsync(appCustomerData);
        await _dbContext.SaveChangesAsync();
        return customer.Entity;
    }

    public async Task<AppCustomer?> GetByEmailOrDocument(GetByEmailOrDocumentQuery query)
    {
        return await _dbContext.AppCustomer
            .FirstOrDefaultAsync(c => c.Email == query.Email || c.Document == query.Document);
    }

    public async Task<AppCustomer?> GetById(Guid id)
    {
        return await _dbContext.AppCustomer
            .FirstOrDefaultAsync(c => c.Id == id || c.UserId == id);
    }

    public async Task<AppCustomer?> Update(AppCustomer customerData)
    {
        var customer = _dbContext.AppCustomer.Update(customerData);
        await _dbContext.SaveChangesAsync();
        return customer.Entity;
    }

    public async void Delete(AppCustomer? customer)
    {
        _dbContext.AppCustomer.Remove(customer);
        await _dbContext.SaveChangesAsync();
    }
}