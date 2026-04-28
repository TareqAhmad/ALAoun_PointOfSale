

namespace ALAoun_Pos.Models
{
    public class PurchaseInvoicesVM
    {
        public int PurchaseId {get; set;}
        public DateTime PurchaseDate {get;set;}
        public string? InvoiceType {get; set;}
        public decimal PurchaseTotal {get;set;}
        public decimal DiscountAmount{ get; set;}
        public decimal TaxAmount {get; set;}
        public decimal NetAmount {get; set;}
        public string? SupplierName {get; set;}
        public string? PaymentType {get;set; }


    }
}