
namespace ALAoun_Pos.Models
{
    public class ClsPurchases
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int SupplierId { get; set; }
        public int PaymentId { get; set; }
        public int InvoiceTypeId {get; set; }
        public int companyId { get; set; }
        public int BranchId { get; set; }

        public List<ClsPurchaseInvoiceItems> purchaseItems {get; set;}
    }
}