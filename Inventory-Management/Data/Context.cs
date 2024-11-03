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
        DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        DbSet<InventoryTransactionArchive> InventoryTransactionArchives { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(
                "Server=.;Database=test123;Trusted_Connection=True;Encrypt=False;"
                )
                .LogTo(log => Console.WriteLine(log))
                .EnableSensitiveDataLogging();
        }

        
        
    }
}
