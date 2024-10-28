using Microsoft.EntityFrameworkCore;

namespace Inventory_Management.Data
{
    public class Context :DbContext
    {
        public Context() { 
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        }


        
    }
}
