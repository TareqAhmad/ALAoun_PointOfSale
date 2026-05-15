
using System.Diagnostics;
using ALAoun_Pos.Models;
using ALAoun_Pos.Data; 
using ALAoun_Pos.Services.interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using AspNetCoreGeneratedDocument;


namespace ALAoun_Pos.Services
{
    public class ProductsService : IProductsService
    {

        private readonly DbHelper _dbHelper; 

        public ProductsService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<ClsProducts> GetAllProducts(int companyId, int branchId)
        {
                List<ClsProducts> products = new List<ClsProducts>();

       
                 string query = @"SELECT ProductId,Barcode,
                                    ProductName,
                                    ProductCost,
                                    ProductPrice,
                                    PurchasePrice,
                                    StockQuantity,
                                    categoryId,
                                    supplierId,
                                    iconId
                                FROM Products
                                WHERE CompanyId = @CompanyId AND BranchId = @BranchId"; // Adjust the table name and columns as needed
            
                 SqlParameter[] parameters =
                {
                    new SqlParameter("@companyId",companyId),
                    new SqlParameter("@branchId",branchId)
                }; 

                DataTable dt = _dbHelper.Select(query,parameters); 

                foreach(DataRow row in dt.Rows)
                {
                     var product = new ClsProducts
                     {
                         ProductId = Convert.ToInt32(row[0]),
                         Barcode = row[1].ToString(),
                         ProductName = row[2]== DBNull.Value ? "not Found" : row[2].ToString(),
                         ProductCost = row[3]== DBNull.Value ? 0 : Convert.ToDecimal(row[3]),
                         ProductPrice = row[4]== DBNull.Value ? 0 :   Convert.ToDecimal(row[4]),
                         PurchasePrice = row[5]== DBNull.Value ? 0 :Convert.ToDecimal(row[5]),
                         StockQuantity = Convert.ToInt32(row[6]),
                         CategoryId = Convert.ToInt32(row[7]),
                         SupplierId = row[8] == DBNull.Value ? 0 :  Convert.ToInt32(row[8]),
                         iconId = row[9] == DBNull.Value ? 0 :  Convert.ToInt32(row[9])

                     }; 

                     products.Add(product);
                }
               
                return products;
          
            }

        public ClsProducts GetProductById(int companyId, int branchId, int id)
        {
           ClsProducts product = null;

            string query = @"SELECT ProductId,Barcode,
                                    ProductName,
                                    ProductCost,
                                    ProductPrice,
                                    StockQuantity,
                                    categoryId,
                                    supplierId,
                                    iconId
                                FROM Products
                                WHERE CompanyId = @CompanyId AND BranchId = @BranchId AND ProductId = @ProductId"; // Adjust the table name and columns as needed
            
                 SqlParameter[] parameters =
                {
                    new SqlParameter("@companyId",companyId),
                    new SqlParameter("@branchId",branchId),
                    new SqlParameter("@ProductId",id)
                }; 

                DataTable dt = _dbHelper.Select(query,parameters); 

                if(dt.Rows.Count > 0)
                {
                     var row = dt.Rows[0];
                     product = new ClsProducts
                     {
                         ProductId = Convert.ToInt32(row[0]),
                         Barcode = row[1].ToString(),
                         ProductName = row[2].ToString(),
                         ProductCost = Convert.ToDecimal(row[3]),
                         ProductPrice =Convert.ToDecimal(row[4]),
                         StockQuantity = Convert.ToInt32(row[5]),
                         CategoryId = Convert.ToInt32(row[6]),
                         SupplierId = row[7] == DBNull.Value ? 0 :  Convert.ToInt32(row[7]),
                         iconId = row[8] == DBNull.Value ? 0 :  Convert.ToInt32(row[8])

                     }; 
                }
               
                return product; 
        }

        public bool AddProduct(ProductDto product)
        {
            string query = @"INSERT INTO Products (Barcode, ProductName, ProductCost, ProductPrice, StockQuantity, CategoryId,TaxId,BaseUnitId,SubUnitId,ConversionFactor,ReorderLevel, SupplierId, CompanyId, BranchId,iconId)
                            VALUES (@Barcode, @ProductName, @ProductCost, @ProductPrice, @StockQuantity, @CategoryId, @TaxId, @BaseUnitId, @SubUnitId, @ConversionFactor, @ReorderLevel, @SupplierId,@CompanyId, @BranchId, @iconId)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Barcode", product.barcode),
                new SqlParameter("@ProductName", product.productName),
                new SqlParameter("@ProductCost", product.productCost),
                new SqlParameter("@ProductPrice", product.productPrice),
                new SqlParameter("@StockQuantity", product.stockQuantity),
                new SqlParameter("@CategoryId", product.categoryId),
                new SqlParameter("@TaxId", product.taxId),
                new SqlParameter("@discountId", product.discountId),
                new SqlParameter("@BaseUnitId", product.baseUnitId),
                new SqlParameter("@SubUnitId", product.subUnitId),
                new SqlParameter("@ConversionFactor", product.conversionFactor),
                new SqlParameter("@SupplierId", product.supplierId),
                new SqlParameter("@ReorderLevel", product.reorderLevel),
                new SqlParameter("@CompanyId", product.companyId),
                new SqlParameter("@BranchId", product.branchId),
                new SqlParameter("@iconId", product.iconId) // Assuming iconId is optional and can be null
            };

            int rowsAffected = _dbHelper.Execute(query, parameters);

            return rowsAffected > 0;
        }

        public bool EditProduct(ClsProducts product)
        {
            // Implementation for editing a product
            return true;
        }

        public bool DeleteProduct(int id)
        {
            // Implementation for deleting a product
            return true;
        }

        public List<ProductForOperationsViewModel> GetInfoProductsForOperation(int companyId, int branchId)
        {
            var products = new List<ProductForOperationsViewModel>(); 

            string query = @"SELECT P.productId,P.barcode,P.productName,P.productPrice,P.purchasePrice,P.productCost,P.iconId,T.taxRate,D.discountRate
                            FROM Products P left JOIN Taxies T ON P.taxId = T.TaxId
                                            left JOIN Discounts D ON P.discountId = D.DiscountId
                            WHERE P.companyId = @companyId
                            AND P.branchId =  @branchId;";

            SqlParameter[] parameters =
            {
                new SqlParameter("@companyId",companyId),
               new SqlParameter("@branchId",branchId)  
            };  

            DataTable dt = _dbHelper.Select(query,parameters); 

            foreach(DataRow row in dt.Rows)
            {
                 var product = new ProductForOperationsViewModel
                 {
                     productId = Convert.ToInt32(row[0]),
                     barcode = row[1] == DBNull.Value ? "no Barcode" : row[1].ToString(),
                     productName = row[2] == DBNull.Value ? "not Fount" : row[2].ToString(),
                     productPrice = row[3] == DBNull.Value ? 0 : Convert.ToDecimal(row[3]),
                     purchasePrice = row[4] == DBNull.Value ? 0 : Convert.ToDecimal(row[4]),
                     productCost = row[5] == DBNull.Value ? 0 : Convert.ToDecimal(row[5]),
                     iconId  = row[6] == DBNull.Value ? 0 : Convert.ToInt32(row[6]),
                     taxRate = row[7] == DBNull.Value ? 0 : Convert.ToDecimal(row[7]),
                     discountRate = row[8] == DBNull.Value ? 0 : Convert.ToDecimal(row[8])
                 };
                  products.Add(product); 
            }

            return products; 

        }
    }
}