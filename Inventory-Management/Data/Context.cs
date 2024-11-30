using Inventory_Management.Common.Enums;
using Inventory_Management.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Inventory_Management.Data
{
    public class Context :DbContext
    {
        public Context() { }
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        

        DbSet<Admin> Admins { get; set; }
        DbSet<Role> Roles { get; set; } 
        DbSet<RoleFeature> RoleFeatures { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<OtpVerification> Otps { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<InventoryTransaction> InventoryTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category() { ID = 1, IsDeleted = false, Name = "Snacks & Sweets" },
                new Category() { ID = 2, IsDeleted = false, Name = "Devices" },
                new Category() { ID = 3, IsDeleted = false, Name = "Grocery" },
                new Category() { ID = 4, IsDeleted = false, Name = "Clothes" },
                new Category() { ID = 5, IsDeleted = false, Name = "Others" });

            builder.Entity<Product>().HasData(
                new Product() { ID = 1, IsDeleted = false, Name = "Maggi", CategoryID = 3, ImageUrl = "http://res.cloudinary.com/dpapfkrx1/image/upload/v1730568418/maggi-noodles-1000x1000.jpg", Price = (decimal)12.00, Quantity = 50, Available = productAvailability.InStock, Unit = "g1", Threshold = 22, ExpiryDate = new DateTime(2025, 10, 27), CreatedAt = DateTime.UtcNow },
                new Product() { ID = 2, IsDeleted = false, Name = "Tiger", CategoryID = 1, ImageUrl = "http://res.cloudinary.com/dpapfkrx1/image/upload/v1730568627/6223000508572_-_37g.webp", Price = (decimal)7.00, Quantity = 40, Available = productAvailability.InStock, Unit = "g1", Threshold = 20, ExpiryDate = new DateTime(2025, 11, 27), CreatedAt = DateTime.UtcNow },
                new Product() { ID = 3, IsDeleted = false, Name = "dexdece", CategoryID = 5, ImageUrl = "http://res.cloudinary.com/dpapfkrx1/image/upload/v1730656198/caregory.jpg", Price = (decimal)250, Quantity = 7, Available = productAvailability.LowStock, Unit = "g1", Threshold = 5, ExpiryDate = new DateTime(2026, 10, 27), CreatedAt = DateTime.UtcNow },
                new Product() { ID = 4, IsDeleted = false, Name = "product2", CategoryID = 5, ImageUrl = "http://res.cloudinary.com/dpapfkrx1/image/upload/v1730658274/01d4fdc0e786083bd7002de356fb29c3.jpg", Price = (decimal)100.0, Quantity = 12, Available = productAvailability.OutOfStock, Unit = "g1", Threshold = 22, ExpiryDate = new DateTime(2025, 01, 21), CreatedAt = DateTime.UtcNow });
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(
                //"Data Source=SQL9001.site4now.net;Initial Catalog=db_aaeba0_env00;User Id=db_aaeba0_env00_admin;Password=Apis_Ups2024"
                // "Server=DESKTOP-GBMV023\\MSSQLSERVER2022;Database=Inventory_Management_System;Trusted_Connection=True;Encrypt=False;"
                 "Server =MSI\\SQLEXPRESS; Database = Inventory_Management; Trusted_Connection = True; TrustServerCertificate = True;"
                )
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
  


    }
}
