namespace Inventory_Management.Features.StaticsInventory
{
    public class Dasboard_dataEndPointRequest
    {
        public Dictionary<int, int> Top_Selling { get; set; }
        public int num_total_product { get; set; }

        public int totalRevenue { get; set; }

        public int Number_NoStock { get; set; }

        public int Number_LowStock { get; set; }

        public int Number_Categories { get; set; }


    }
}
