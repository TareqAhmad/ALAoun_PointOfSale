
namespace ALAoun_Pos.Models
{
    public class ClsTenants
    {
        public int TenantId { get; set; }
        public string? TenantName { get; set; }
        public string? SubscriptionType { get; set; }
        public DateTime SubscriptionStart { get; set; }
        public DateTime SubscriptionEnd { get; set; }
        public bool IsActive {get; set;}
   
    }
}