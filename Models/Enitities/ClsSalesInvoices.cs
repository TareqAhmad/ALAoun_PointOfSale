
namespace ALAoun_Pos.Models
{
    public class ClsSalesInvoices
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }

        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
   
        public int QuantityItems{ get; set; }

        public  int InvoiceTypeId { get; set; }
        public int PaymentId { get; set; }

        public int companyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }

        public int PosId { get; set; }

    }   
}