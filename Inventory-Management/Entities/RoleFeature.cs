namespace Inventory_Management.Entities
{
    public class RoleFeature :BaseModel
    {
        public Role Role { get; set; }

        public int RoleId { get; set; } 

        public Feature Feature { get; set; }

        public int FeatureId { get; set; }  
    }
}
