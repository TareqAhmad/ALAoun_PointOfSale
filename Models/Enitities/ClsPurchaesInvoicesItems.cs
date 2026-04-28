

namespace ALAoun_Pos.Models
{
    public class ClsPurchaseInvoiceItems
    {
        public int ItemId { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAfterTax { get; set; }
    }
}