﻿namespace Inventory_Management.Entities
{
    public class User:BaseModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime CreationAt { get; set; } = DateTime.Now;
        public DateTime? LastLogin { get; set; }
        public string? OtpCode { get; set; }
        public DateTime? OtpExpiry { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}