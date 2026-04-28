

namespace ALAoun_Pos.Models
{
    public class ClsCustomers
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }  
        public DateTime CreatedAt { get; set; }

    }
}