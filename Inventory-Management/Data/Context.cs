﻿using Inventory_Management.Common.Enums;
using Inventory_Management.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Inventory_Management.Data
{
    public class Context :DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        DbSet<Admin> Admins { get; set; }
        DbSet<Role> Roles { get; set; } 
        DbSet<RoleFeature> RoleFeatures { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<OtpVerification> Otps { get; set; }
        DbSet<Product> Products { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<InventoryTransaction> InventoryTransactions { get; set; }

        public Context()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Server=Server=DESKTOP-GBMV023\\MSSQLSERVER2022;Database=Inventory_Management_System;Trusted_Connection=True;Encrypt=False;")

                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            }

           
        
    }
}
