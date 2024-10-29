namespace Inventory_Management.Features.Users.ChangePassword
{
    public class ChangePasswordEndPointRequest
    {
        public string newPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string oldPassword { get; set; }
    }
}
