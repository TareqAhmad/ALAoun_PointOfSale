
namespace ALAoun_Pos.Models
{
    public class ClsSuppliers
    {
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }  
    }
}