using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    

    public interface ISuppliersService
    {
        
       public List<ClsSuppliers> GetAllSuppliers(int companyId,int branchId); 


       public ClsSuppliers GetSupplierById(int companyId,int branchId,int customerId);


       public bool AddSupplier(); 

       public bool EditSupplier(ClsSuppliers supplier); 

       public bool DeleteSupplier(ClsSuppliers supplier); 

    }
}