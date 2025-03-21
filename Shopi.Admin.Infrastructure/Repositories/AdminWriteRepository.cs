using Shopi.Admin.Domain.Entities;
using Shopi.Admin.Domain.Interfaces;
using Shopi.Admin.Infrastructure.Data;

namespace Shopi.Admin.Infrastructure.Repositories;

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