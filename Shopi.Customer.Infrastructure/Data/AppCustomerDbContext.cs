using Microsoft.EntityFrameworkCore;
using Shopi.Customer.Domain.Entities;

namespace Shopi.Customer.Infrastructure.Data;

public class AppCustomerDbContext : DbContext
{
    public AppCustomerDbContext(DbContextOptions<AppCustomerDbContext> options) : base(options)
    {
    }

    public DbSet<AppCustomer> AppCustomer { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AppModelBuilder.AddModels(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}