

namespace ALAoun_Pos.Models
{
    public class ClsStockMovements
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public Decimal Quantity { get; set; }
        public string? MovementType { get; set; }
        public int ReferenceId { get; set; }
        public DateTime MovementDate { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }

    }
}
