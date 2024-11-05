﻿using Inventory_Management.Common.Enums;
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
        DbSet<InventoryTransaction> InventoryTransactions { get; set; }


        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(
                "Data Source=SQL9001.site4now.net;Initial Catalog=db_aaeba0_env00;User Id=db_aaeba0_env00_admin;Password=Apis_Ups2024"
                // "Server=DESKTOP-GBMV023\\MSSQLSERVER2022;Database=Inventory_Management_System;Trusted_Connection=True;Encrypt=False;"
                 //"Server =MSI\\SQLEXPRESS; Database = Inventory_Management; Trusted_Connection = True; TrustServerCertificate = True;"
                )
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        
        
    }
}
