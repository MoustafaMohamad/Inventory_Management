using Inventory_Management.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Management.Common.Helpers
{
    public class QueryObject
    {
        public string? Name { get; set; }
        public productAvailability? Available { get; set; }
        [Required]
        public int PageNumbar { get; set; }
        [Required]
        public int PageSize { get; set; } 
    }
}
