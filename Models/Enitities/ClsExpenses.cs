
namespace ALAoun_Pos.Models
{
    public class ClsExpenses
    {
        public int expenseId { get; set; }
        public DateTime expenseDate {get; set; }
        public string? description { get; set; }
        public decimal amount { get; set; }
        public int companyId { get; set; }
        public int BranchId { get; set; }

    }
}