
namespace ALAoun_Pos.Models
{
    public class ClsCarts
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
    }
}