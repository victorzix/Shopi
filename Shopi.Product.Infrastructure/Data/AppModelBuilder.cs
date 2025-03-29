using Microsoft.EntityFrameworkCore;
using Shopi.Product.Domain.Entities;

namespace Shopi.Product.Infrastructure.Data;

public static class AppModelBuilder
{
    public static void AddModels(ModelBuilder mb)
    {
        mb.Entity<AppProductCategory>(entity =>
        {
            entity.HasKey(pc => new { pc.ProductId, pc.CategoryId });

            entity.HasOne(pc => pc.AppProduct)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            entity.HasOne(pc => pc.Category)
                .WithMany(c => c.AppProductCategories)
                .HasForeignKey(pc => pc.CategoryId);
        });


        mb.Entity<Review>(entity =>
        {
            entity.HasOne(r => r.AppProduct)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.AppProductId).OnDelete(DeleteBehavior.Cascade);

            entity.Property(r => r.Title).HasColumnType("varchar(45)");
            entity.Property(r => r.Comment).HasColumnType("text");
        });

        mb.Entity<ReviewResponses>(entity =>
        {
            entity.HasOne(rr => rr.Review)
                .WithMany(r => r.Responses)
                .HasForeignKey(rr => rr.ReviewId).OnDelete(DeleteBehavior.Cascade);

            entity.Property(rr => rr.Comment).HasColumnType("text");
        });

        mb.Entity<AppProduct>(entity =>
        {
            entity.HasIndex(p => p.Sku).IsUnique();

            entity.Property(p => p.Sku).HasColumnType("varchar(18)");
            entity.Property(p => p.Description).HasColumnType("text");
            entity.Property(p => p.Manufacturer).HasColumnType("varchar(30)");
            entity.Property(p => p.Name).HasColumnType("varchar(60)");
        });

        mb.Entity<Category>(entity =>
        {
            entity.Property(c => c.Description).HasColumnType("text");
            entity.Property(c => c.Name).HasColumnType("varchar(60)");
        });
    }
}