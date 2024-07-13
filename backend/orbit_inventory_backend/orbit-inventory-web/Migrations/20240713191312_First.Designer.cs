﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using orbit_inventory_data;

#nullable disable

namespace orbit_inventory_web.Migrations
{
    [DbContext(typeof(OrbitDbContext))]
    [Migration("20240713191312_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("orbit_inventory_core.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("salt");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("orbit_inventory_domain.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Available")
                        .HasColumnType("boolean")
                        .HasColumnName("available");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedById")
                        .HasColumnType("integer")
                        .HasColumnName("created_by_id");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int?>("PurchaseLineId")
                        .HasColumnType("integer")
                        .HasColumnName("purchase_line_id");

                    b.Property<int>("SupplierId")
                        .HasColumnType("integer")
                        .HasColumnName("supplier_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_inventory");

                    b.HasIndex("CreatedById")
                        .HasDatabaseName("ix_inventory_created_by_id");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_inventory_product_id");

                    b.HasIndex("PurchaseLineId")
                        .HasDatabaseName("ix_inventory_purchase_line_id");

                    b.HasIndex("SupplierId")
                        .HasDatabaseName("ix_inventory_supplier_id");

                    b.ToTable("inventory", (string)null);
                });

            modelBuilder.Entity("orbit_inventory_domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedById")
                        .HasColumnType("integer")
                        .HasColumnName("created_by_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Upc")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("upc");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_product");

                    b.HasIndex("CreatedById")
                        .HasDatabaseName("ix_product_created_by_id");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("orbit_inventory_domain.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedById")
                        .HasColumnType("integer")
                        .HasColumnName("created_by_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("SupplierId")
                        .HasColumnType("integer")
                        .HasColumnName("supplier_id");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_purchase");

                    b.HasIndex("CreatedById")
                        .HasDatabaseName("ix_purchase_created_by_id");

                    b.HasIndex("SupplierId")
                        .HasDatabaseName("ix_purchase_supplier_id");

                    b.ToTable("purchase", (string)null);
                });

            modelBuilder.Entity("orbit_inventory_domain.PurchaseLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int?>("PurchaseId")
                        .HasColumnType("integer")
                        .HasColumnName("purchase_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("double precision")
                        .HasColumnName("unit_price");

                    b.HasKey("Id")
                        .HasName("pk_purchase_line");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_purchase_line_product_id");

                    b.HasIndex("PurchaseId")
                        .HasDatabaseName("ix_purchase_line_purchase_id");

                    b.ToTable("purchase_line", (string)null);
                });

            modelBuilder.Entity("orbit_inventory_domain.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedById")
                        .HasColumnType("integer")
                        .HasColumnName("created_by_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_supplier");

                    b.HasIndex("CreatedById")
                        .HasDatabaseName("ix_supplier_created_by_id");

                    b.ToTable("supplier", (string)null);
                });

            modelBuilder.Entity("orbit_inventory_domain.Inventory", b =>
                {
                    b.HasOne("orbit_inventory_core.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_inventory_user_created_by_id");

                    b.HasOne("orbit_inventory_domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_inventory_product_product_id");

                    b.HasOne("orbit_inventory_domain.PurchaseLine", null)
                        .WithMany("Items")
                        .HasForeignKey("PurchaseLineId")
                        .HasConstraintName("fk_inventory_purchase_line_purchase_line_id");

                    b.HasOne("orbit_inventory_domain.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_inventory_supplier_supplier_id");

                    b.Navigation("CreatedBy");

                    b.Navigation("Product");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("orbit_inventory_domain.Product", b =>
                {
                    b.HasOne("orbit_inventory_core.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_user_created_by_id");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("orbit_inventory_domain.Purchase", b =>
                {
                    b.HasOne("orbit_inventory_core.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_purchase_user_created_by_id");

                    b.HasOne("orbit_inventory_domain.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_purchase_supplier_supplier_id");

                    b.Navigation("CreatedBy");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("orbit_inventory_domain.PurchaseLine", b =>
                {
                    b.HasOne("orbit_inventory_domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_purchase_line_product_product_id");

                    b.HasOne("orbit_inventory_domain.Purchase", null)
                        .WithMany("Lines")
                        .HasForeignKey("PurchaseId")
                        .HasConstraintName("fk_purchase_line_purchase_purchase_id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("orbit_inventory_domain.Supplier", b =>
                {
                    b.HasOne("orbit_inventory_core.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supplier_user_created_by_id");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("orbit_inventory_domain.Purchase", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("orbit_inventory_domain.PurchaseLine", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
