

namespace ALAoun_Pos.Models
{
    public class ProductDto
    {
        public string? barcode { get; set; }
        public string? productName { get; set; }
        public decimal productPrice { get; set; }
        public decimal productCost { get; set; }
        public int stockQuantity { get; set; }
         public int categoryId { get; set; }
         public int taxId {get; set; }
         public int discountId { get; set; }
         public int baseUnitId { get; set; }
         public int subUnitId { get; set; }
        public decimal conversionFactor { get; set; }
        public int reorderLevel { get; set; }  
        public int supplierId { get; set; }  
        public int companyId { get; set; }
        public int branchId { get; set; }
         public int iconId {get; set; }
    }
}