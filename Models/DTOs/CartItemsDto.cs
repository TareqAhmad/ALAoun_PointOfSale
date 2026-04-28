

namespace ALAoun_Pos.Models
{
    public class CartItemDto
{

    public int itemId {get; set; }
    public int productId { get; set; }
    public string? productName { get; set; }
    public decimal price { get; set; }
    public int quantity { get; set; }
}
}