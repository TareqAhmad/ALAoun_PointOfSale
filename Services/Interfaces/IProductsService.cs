using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces 
    
{
    public interface IProductsService
    {

        public List<ClsProducts> GetAllProducts(int companyId, int branchId);

        public ClsProducts GetProductById(int companyId, int branchId, int id);

        public bool AddProduct(ClsProducts product);

        public bool EditProduct(ClsProducts product);

        public bool DeleteProduct(int id);


    }
}