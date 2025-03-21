using Microsoft.EntityFrameworkCore;
using Shopi.Admin.Domain.Entities;

namespace Shopi.Admin.Infrastructure.Data;

public static class AppModelBuilder
{
    public static void AddModels(ModelBuilder mb)
    {
        mb.Entity<AppAdmin>().HasIndex(c => c.Email).IsUnique();
        mb.Entity<AppAdmin>().HasIndex(c => c.UserId).IsUnique();
    }
}