
namespace ALAoun_Pos.Models
{
    public class ClsCompanies
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? TaxNumber { get; set; }
        public string? ClientId { get; set; }
        public string? SecretKey { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int TenantId { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } 


    }
}