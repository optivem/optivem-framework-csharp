﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Optivem.Template.Infrastructure.EntityFrameworkCore;

namespace Optivem.Template.Infrastructure.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190928164949_ProductRecord_Added_IsActive")]
    partial class ProductRecord_Added_IsActive
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Customers.CustomerRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderDetailRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("Quantity");

                    b.Property<int>("StatusId");

                    b.Property<byte?>("StatusId1");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("StatusId1");

                    b.ToTable("OrderDetailRecord");
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderDetailStatusRecord", b =>
                {
                    b.Property<byte>("Id");

                    b.Property<string>("Code");

                    b.HasKey("Id");

                    b.ToTable("OrderDetailStatusRecord");

                    b.HasData(
                        new
                        {
                            Id = (byte)0,
                            Code = "None"
                        },
                        new
                        {
                            Id = (byte)1,
                            Code = "Allocated"
                        },
                        new
                        {
                            Id = (byte)2,
                            Code = "Invoiced"
                        },
                        new
                        {
                            Id = (byte)3,
                            Code = "Shipped"
                        },
                        new
                        {
                            Id = (byte)4,
                            Code = "OnOrder"
                        },
                        new
                        {
                            Id = (byte)5,
                            Code = "NoStock"
                        });
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId");

                    b.Property<byte>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StatusId");

                    b.ToTable("OrderRecord");
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderStatusRecord", b =>
                {
                    b.Property<byte>("Id");

                    b.Property<string>("Code");

                    b.HasKey("Id");

                    b.ToTable("OrderStatusRecord");

                    b.HasData(
                        new
                        {
                            Id = (byte)0,
                            Code = "None"
                        },
                        new
                        {
                            Id = (byte)1,
                            Code = "New"
                        },
                        new
                        {
                            Id = (byte)2,
                            Code = "Invoiced"
                        },
                        new
                        {
                            Id = (byte)3,
                            Code = "Shipped"
                        },
                        new
                        {
                            Id = (byte)4,
                            Code = "Closed"
                        },
                        new
                        {
                            Id = (byte)7,
                            Code = "Submitted"
                        },
                        new
                        {
                            Id = (byte)8,
                            Code = "Cancelled"
                        },
                        new
                        {
                            Id = (byte)9,
                            Code = "Archived"
                        });
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Products.ProductRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<decimal>("ListPrice");

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductName");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderDetailRecord", b =>
                {
                    b.HasOne("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderRecord", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Optivem.Template.Infrastructure.EntityFrameworkCore.Products.ProductRecord", "Product")
                        .WithMany("OrderDetail")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderDetailStatusRecord", "Status")
                        .WithMany("OrderDetail")
                        .HasForeignKey("StatusId1");
                });

            modelBuilder.Entity("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderRecord", b =>
                {
                    b.HasOne("Optivem.Template.Infrastructure.EntityFrameworkCore.Customers.CustomerRecord", "Customer")
                        .WithMany("Order")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Optivem.Template.Infrastructure.EntityFrameworkCore.Orders.OrderStatusRecord", "Status")
                        .WithMany("Order")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
