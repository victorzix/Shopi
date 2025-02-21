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

    public AppCustomer Create(AppCustomer appCustomerData)
    {
        var customer= _dbContext.Add(appCustomerData);
        _dbContext.SaveChanges();
        return customer.Entity;
    }
    
    public AppCustomer? GetByEmailOrDocument(GetByEmailOrDocumentQuery query)
    {
        return _dbContext.AppCustomer
            .FirstOrDefault(c => c.Email == query.Email || c.Document == query.Document);
    }
    
    public AppCustomer? GetById(Guid id)
    {
        return _dbContext.AppCustomer
            .FirstOrDefault(c => c.Id == id || c.UserId == id);
    }

    public AppCustomer Update(AppCustomer customerData)
    {
        var customer = _dbContext.AppCustomer.Update(customerData);
        _dbContext.SaveChanges();
        return customer.Entity;
    }

    public void Delete(AppCustomer customer)
    {
        _dbContext.AppCustomer.Remove(customer);
        _dbContext.SaveChanges();
    }
}