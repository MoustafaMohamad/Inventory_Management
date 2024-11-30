using System.Numerics;

namespace Inventory_Management.Common.Helpers
{
    public class PaginationResponse<T>
    {
        public int  TotalPagesNumber { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
