namespace ALAoun_Pos.Services.CalculateInvoices
{
    
    public class InvoiceResult
    {
        public decimal TotalSubtotal { get; set; } = 0;
        public decimal TotalDiscount { get; set; } = 0;
        public decimal TotalTaxableAmount { get; set; } = 0;
        public decimal TotalTaxAmount { get; set; } = 0;
        public decimal FinalGrandTotal { get; set; } = 0;

        public List<CalculatedInvoiceItem> CalculatedItems { get; set; } = new List<CalculatedInvoiceItem>();
    }

    public class CalculatedInvoiceItem
    {
        public int ProductId { get; set; }
        public decimal LineSubtotal { get; set; } // Qty * Price
        public decimal LineDiscountAmount { get; set; }
        public decimal LineTaxAmount { get; set; }
        public decimal LineTotal { get; set; } // المبلغ النهائي لهذا السطر
    }
}