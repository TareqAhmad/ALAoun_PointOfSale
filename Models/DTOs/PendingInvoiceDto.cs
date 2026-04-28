

namespace ALAoun_Pos.Models
{
     public class PendingInvoiceDto  // DTO Data Transfer Object
    {
        public int customerId {get; set;}
        public List<CartItemDto>? Items {get; set;}
        public decimal sumAmount { get; set; }
        public decimal sumDiscount { get; set; }
        public decimal sumTax { get; set; }
        public decimal netInvoice { get; set; }

    }
}