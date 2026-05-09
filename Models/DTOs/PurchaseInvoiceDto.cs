namespace ALAoun_Pos.Models
{
    public class PurchaseInvoiceDto
    {
        public DateTime purchaseDate {get;set;}
        public decimal PurchaseTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int SupplierId { get; set; }
        public int PaymentId { get; set; }
        public int CompanyId {get; set;}
        public int branchId {get;set;}
        public int posId {get;set;}
        public int userId {get; set;}

        // قائمة العناصر المرسلة من الجدول في المتصفح
        public List<PurchaseItemDto> Items { get; set; } = new List<PurchaseItemDto>();
    }

    public class PurchaseItemDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAfterTax { get; set; }
    }
}