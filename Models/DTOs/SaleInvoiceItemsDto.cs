namespace ALAoun_Pos.Models
{
     public class SalesInvoiceItemsDto  // DTO Data Transfer Object
    {

        public int itemId {get; set;}
        public int invoiceId {get; set;}
        public int productId {get; set;}
        public int quantity {get; set;}
        public int discountId {get; set;}
        public int taxId {get; set;}    
        public decimal UnitPrice {get; set;}

    }
}