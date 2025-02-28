using Microsoft.EntityFrameworkCore;
using Shopi.Admin.API.Models;

namespace Shopi.Admin.API.Data;

public class AppAdminDbContext : DbContext
{
    public AppAdminDbContext(DbContextOptions<AppAdminDbContext> options) : base(options)
    {
    }

    public DbSet<AppAdmin> AppAdmin { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AppModelBuilder.AddModels(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}