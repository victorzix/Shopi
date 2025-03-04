using Shopi.Admin.API.Data;
using Shopi.Admin.API.Interfaces;
using Shopi.Admin.API.Models;

namespace Shopi.Admin.API.Repositories;

public class AdminWriteRepository : IAdminWriteRepository
{
    private readonly AppAdminDbContext _dbContext;

    public AdminWriteRepository(AppAdminDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppAdmin> Create(AppAdmin appAdminData)
    {
        var customer = await _dbContext.AddAsync(appAdminData);
        await _dbContext.SaveChangesAsync();
        return customer.Entity;
    }

    public async Task<AppAdmin?> Update(AppAdmin customerData)
    {
        var customer = _dbContext.AppAdmin.Update(customerData);
        await _dbContext.SaveChangesAsync();
        return customer.Entity;
    }

    public async Task Delete(AppAdmin customer)
    {
        _dbContext.AppAdmin.Remove(customer);
        await _dbContext.SaveChangesAsync();
    }
}