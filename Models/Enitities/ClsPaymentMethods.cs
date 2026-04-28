

namespace ALAoun_Pos.Models
{
    public class ClsPaymentMethods
    {
        public int PaymentId { get; set; }
        public string? PaymentType { get; set; }
        public string? Description { get; set; }
        public int IsActive { get; set; }
        public int CompanyId { get; set; }
    }
}