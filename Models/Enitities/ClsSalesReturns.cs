

namespace ALAoun_Pos.Models
{
    public class ClsSalesReturns
    {
        public int ReturnId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? ReturnReason { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int PosId { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
    }
}