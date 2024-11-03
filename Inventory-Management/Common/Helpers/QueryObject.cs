using Inventory_Management.Common.Enums;

namespace Inventory_Management.Common.Helpers
{
    public class QueryObject
    {
        public string? Name { get; set; }
        public Category? Category { get; set; }
        public productAvailability? Available { get; set; }

        public int PageNumbar { get; set; } 
        public int PageSize { get; set; } 
    }
}
