﻿using Microsoft.EntityFrameworkCore;
using Shopi.Customer.API.Data;
using Shopi.Customer.API.Interfaces;
using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Repository;

public class AddressWriteRepository : IAddressWriteRepository
{
    public readonly AppCustomerDbContext _dbContext;

    public AddressWriteRepository(AppCustomerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Address> Create(Address address)
    {
        var createdAddress = await _dbContext.Addresses.AddAsync(address);
        await _dbContext.SaveChangesAsync();
        return createdAddress.Entity;
    }

    public async Task<Address?> Update(Address address)
    {
        var updatedAddress = _dbContext.Addresses.Update(address);
        await _dbContext.SaveChangesAsync();
        return updatedAddress.Entity;
    }

    public async Task Delete(Address address)
    {
        _dbContext.Addresses.Remove(address);
        await _dbContext.SaveChangesAsync();
    }
}