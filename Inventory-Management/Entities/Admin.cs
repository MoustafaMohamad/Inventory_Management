namespace Inventory_Management.Entities
{
    public class Admin :BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; } 
        public User User { get; set; }
    }
}
