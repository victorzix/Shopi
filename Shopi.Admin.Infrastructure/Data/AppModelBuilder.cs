using Microsoft.EntityFrameworkCore;
using Shopi.Admin.Domain.Entities;

namespace Shopi.Admin.Infrastructure.Data;

public static class AppModelBuilder
{
    public static void AddModels(ModelBuilder mb)
    {
        mb.Entity<AppAdmin>(entity =>
        {
            entity.HasIndex(a => a.Email).IsUnique();
            entity.Property(a => a.Email).IsRequired().HasColumnType("varchar(100)");

            entity.HasIndex(a => a.UserId).IsUnique();

            entity.Property(a => a.Name).IsRequired().HasColumnType("varchar(50)");
            entity.Property(a => a.ImageUrl).HasColumnType("varchar(255)");
        });
    }
}