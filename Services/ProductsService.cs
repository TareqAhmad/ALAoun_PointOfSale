
using ALAoun_Pos.Models;
using ALAoun_Pos.Data; 
using ALAoun_Pos.Services.interfaces;
using Microsoft.Data.SqlClient;
using System.Data;


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
                         ProductName = row[2].ToString(),
                         ProductCost = Convert.ToDecimal(row[3]),
                         ProductPrice =Convert.ToDecimal(row[4]),
                         StockQuantity = Convert.ToInt32(row[5]),
                         CategoryId = Convert.ToInt32(row[6]),
                         SupplierId = row[7] == DBNull.Value ? 0 :  Convert.ToInt32(row[7]),
                         iconId = row[8] == DBNull.Value ? 0 :  Convert.ToInt32(row[8])

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

        public bool AddProduct(ClsProducts product)
        {
            // Implementation for adding a product
            return false; 
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
    }
}