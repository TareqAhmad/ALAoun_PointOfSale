namespace ALAoun_Pos.Models
{
    public class InvoiceDto
    {
        public DateTime InvoiceDate {get;set;}
        public int PersonId {get;set;} = 0; 
        public int PaymentMethodId { get; set; } = 0; 
        public int CompanyId {get; set;}
        public int BranchId {get;set;}
        public int PosId {get;set;}
        public int UserId {get; set;}

        // قائمة العناصر المرسلة من الجدول في المتصفح
        public List<InvoiceItemDto> Items { get; set; } = new List<InvoiceItemDto>();
    }

    public class InvoiceItemDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal DiscountPercent { get; set; }

    }
}