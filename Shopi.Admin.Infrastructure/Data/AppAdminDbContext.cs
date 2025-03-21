using Microsoft.EntityFrameworkCore;
using Shopi.Admin.Domain.Entities;

namespace Shopi.Admin.Infrastructure.Data;

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