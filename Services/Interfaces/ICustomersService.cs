using ALAoun_Pos.Models;
using ALAoun_POS.Models;

namespace ALAoun_Pos.Services.interfaces
{
    
    public interface ICustomersService
    {

       public List<ClsCustomers> GetAllCustomers(int companyId,int branchId); 


       public ClsCustomers GetCustomerById(int companyId,int branchId,int customerId);


       public bool AddCustomer(CustomerDto customerDto); 

       public bool EditCustomer(CustomerDto customer); 
    
       public bool DeleteCustomer(CustomerDto customer); 



    }
}