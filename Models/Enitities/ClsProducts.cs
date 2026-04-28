

namespace ALAoun_Pos.Models
{
    public class ClsProducts
    {
        public int ProductId { get; set; }
        public string? Barcode { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductCost { get; set; }
        public int StockQuantity { get; set; }
         public int CategoryId { get; set; }
         public int TaxId {get; set; }
        public int SupplierId { get; set; }  
        public int CompanyId { get; set; }
        public int BranchId { get; set; }  
        public int iconId {get; set; }
    }
}