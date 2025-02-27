using Microsoft.EntityFrameworkCore;
using Shopi.Product.API;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.Data;

public class AppProductDbContext : DbContext
{
    public AppProductDbContext(DbContextOptions<AppProductDbContext> options) : base(options)
    {
    }

    public DbSet<AppProduct> AppProducts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<AppProductCategory> AppProductCategories { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ReviewResponses> ReviewResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AppModelBuilder.AddModels(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}