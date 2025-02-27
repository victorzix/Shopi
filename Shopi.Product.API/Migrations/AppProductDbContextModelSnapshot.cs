﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Shopi.Product.API.Data;

#nullable disable

namespace Shopi.Product.API.Migrations
{
    [DbContext(typeof(AppProductDbContext))]
    partial class AppProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Shopi.Product.API.Models.AppProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.PrimitiveCollection<List<string>>("Images")
                        .HasColumnType("text[]");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("Sku")
                        .HasColumnType("text");

                    b.Property<bool>("Visible")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("AppProducts");
                });

            modelBuilder.Entity("Shopi.Product.API.Models.AppProductCategory", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("AppProductCategories");
                });

            modelBuilder.Entity("Shopi.Product.API.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Visible")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Shopi.Product.API.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PostingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<bool>("Visible")
                        .HasColumnType("boolean");

                    b.Property<int?>("Voting")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppProductId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Shopi.Product.API.Models.ReviewResponses", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PostingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Visible")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.ToTable("ReviewResponses");
                });

            modelBuilder.Entity("Shopi.Product.API.Models.AppProductCategory", b =>
                {
                    b.HasOne("Shopi.Product.API.Models.Category", "Category")
                        .WithMany("AppProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shopi.Product.API.Models.AppProduct", "AppProduct")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppProduct");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Shopi.Product.API.Models.Review", b =>
                {
                    b.HasOne("Shopi.Product.API.Models.AppProduct", "AppProduct")
                        .WithMany("Reviews")
                        .HasForeignKey("AppProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppProduct");
                });

            modelBuilder.Entity("Shopi.Product.API.Models.ReviewResponses", b =>
                {
                    b.HasOne("Shopi.Product.API.Models.Review", "Review")
                        .WithMany("Responses")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });

            modelBuilder.Entity("Shopi.Product.API.Models.AppProduct", b =>
                {
                    b.Navigation("ProductCategories");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Shopi.Product.API.Models.Category", b =>
                {
                    b.Navigation("AppProductCategories");
                });

            modelBuilder.Entity("Shopi.Product.API.Models.Review", b =>
                {
                    b.Navigation("Responses");
                });
#pragma warning restore 612, 618
        }
    }
}
