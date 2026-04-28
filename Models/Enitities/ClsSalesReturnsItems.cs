
namespace ALAoun_Pos.Models
{
    public class ClsSalesReturnItems
    {
        public int ItemId { get; set; }
        public int ReturnId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int TaxId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}