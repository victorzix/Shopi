using Microsoft.EntityFrameworkCore;
using Shopi.Customer.Domain.Entities;

namespace Shopi.Customer.Infrastructure.Data;

public static class AppModelBuilder
{
    public static void AddModels(ModelBuilder mb)
    {
        mb.Entity<AppCustomer>(entity =>
        {
            entity.HasMany(c => c.Addresses).WithOne(a => a.AppCustomer).HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(c => c.Email).HasColumnType("varchar(100)");

            entity.Property(c => c.Document).HasColumnType("varchar(25)");

            entity.HasIndex(c => c.UserId).IsUnique();

            entity.Property(a => a.Name).IsRequired().HasColumnType("varchar(50)");
        });
    }
}