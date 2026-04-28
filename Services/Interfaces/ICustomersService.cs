using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    
    public interface ICustomersService
    {

       public List<ClsCustomers> GetAllCustomers(int companyId,int branchId); 


       public ClsCustomers GetCustomerById(int companyId,int branchId,int customerId);


       public bool AddCustomer(); 

       public bool EditCustomer(ClsCustomers customer); 

       public bool DeleteCustomer(ClsCustomers customer); 



    }
}