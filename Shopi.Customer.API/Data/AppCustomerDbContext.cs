using Microsoft.EntityFrameworkCore;
using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Data;

public class AppCustomerDbContext : DbContext
{
    public AppCustomerDbContext(DbContextOptions<AppCustomerDbContext> options) : base(options)
    {
    }

    public DbSet<AppCustomer?> AppCustomer { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AppModelBuilder.AddModels(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}