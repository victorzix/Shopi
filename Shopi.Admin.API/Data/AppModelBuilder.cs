using Microsoft.EntityFrameworkCore;
using Shopi.Admin.API.Models;

namespace Shopi.Admin.API.Data;

public static class AppModelBuilder
{
    public static void AddModels(ModelBuilder mb)
    {
        mb.Entity<AppAdmin>().HasIndex(c => c.Email).IsUnique();
        mb.Entity<AppAdmin>().HasIndex(c => c.UserId).IsUnique();
    }
}