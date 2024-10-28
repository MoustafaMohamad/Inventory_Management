namespace Inventory_Management.Entities
{
    public class User
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int RoleID { get; set; }

        public Role Role { get; set; }
    }
}
