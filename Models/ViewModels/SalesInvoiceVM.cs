

namespace ALAoun_Pos.Models
{
    public class SalesInvoiceVM
   {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? InvoiceType { get; set; }
        public string? PaymentType { get; set; }
        public string? CustomerName { get; set; }
        public int QuantityItems { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string? userName { get; set; }
   }
}