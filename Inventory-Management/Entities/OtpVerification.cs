namespace Inventory_Management.Entities
{
    public class OtpVerification : BaseModel
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
