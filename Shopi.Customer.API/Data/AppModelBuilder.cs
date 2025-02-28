using Microsoft.EntityFrameworkCore;
using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Data;

public static class AppModelBuilder
{
    public static void AddModels(ModelBuilder mb)
    {
        mb.Entity<AppCustomer>().HasMany(c => c.Addresses).WithOne(a => a.AppCustomer).HasForeignKey(a => a.CustomerId)
            .OnDelete((DeleteBehavior.Cascade));
        mb.Entity<AppCustomer>().HasIndex(c => c.Email).IsUnique();
        mb.Entity<AppCustomer>().HasIndex(c => c.Document).IsUnique();
        mb.Entity<AppCustomer>().HasIndex(c => c.UserId).IsUnique();
    }
}