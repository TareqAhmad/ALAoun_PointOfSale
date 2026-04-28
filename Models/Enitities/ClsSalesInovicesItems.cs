
namespace ALAoun_Pos.Models
{
    public class ClsSalesInvoiceItems
    {
        public int ItemId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public int DiscountId { get; set; }
        public int TaxId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}