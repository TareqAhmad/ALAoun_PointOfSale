

namespace ALAoun_Pos.Models
{
     
     public class ProductForOperationsViewModel
    {
        public int productId {get;set;}
        public string? barcode {get;set;}
        public string? productName {get;set;}
        public decimal productPrice {get;set;}
        public decimal purchasePrice {get;set;}
        public decimal productCost {get;set;}
        public decimal taxRate {get;set;}
        public decimal discountRate {get;set;}

    }


}