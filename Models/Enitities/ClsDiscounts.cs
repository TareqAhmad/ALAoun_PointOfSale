

namespace ALAoun_Pos.Models
{
    public class ClsDiscounts
    {
        public int discountId { get; set; }
        public string? discountName { get; set; }
        public string? discountType { get; set; }
        public decimal discountRate{ get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int companyId { get; set; }
        public int branchId { get; set; }
    }
}