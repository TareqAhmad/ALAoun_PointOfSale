namespace ALAoun_Pos.Models
{
     
     public class CartItemsViewModel
    {
        public int itemId {get; set; }
        public int productId { get; set; }  
        public string? productName { get; set; }
        public int quantity { get; set; }
        public decimal discountRate { get; set; }

        public decimal taxRate { get; set; }
        public decimal unitPrice { get; set; }

        public decimal totalPrice { get; set; }



    }


}
