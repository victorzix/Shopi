using Microsoft.EntityFrameworkCore;
using Shopi.Product.API;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.Data;

public static class AppModelBuilder
{
    public static void AddModels(ModelBuilder mb)
    {
        mb.Entity<AppProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });

        mb.Entity<AppProductCategory>()
            .HasOne(pc => pc.AppProduct)
            .WithMany(p => p.ProductCategories)
            .HasForeignKey(pc => pc.ProductId);

        mb.Entity<AppProductCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(c => c.AppProductCategories)
            .HasForeignKey(pc => pc.CategoryId);

        mb.Entity<Review>()
            .HasOne(r => r.AppProduct)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.AppProductId);

        mb.Entity<ReviewResponses>()
            .HasOne(rr => rr.Review)
            .WithMany(r => r.Responses)
            .HasForeignKey(rr => rr.ReviewId);
    }
}