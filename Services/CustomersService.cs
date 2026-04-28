using Microsoft.Data.SqlClient;
using System.Data;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;
using ALAoun_Pos.Data; 

namespace ALAoun_Pos.Services
{
    
    public class CustomersService :ICustomersService
    {
        
           private readonly DbHelper _dbHelper; 

           public CustomersService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
       public List<ClsCustomers> GetAllCustomers(int companyId,int branchId)
        {
            List<ClsCustomers> customers  = new List<ClsCustomers>();
             
            string query = @"SELECT CustomerId,CustomerName,Phone,Email,Address
                             FROM Customers
                             WHERE CompanyId =@companyId
                             AND BranchId = @branchId";

           SqlParameter[] parameters =
            {
                new SqlParameter("@companyId",companyId),
                new SqlParameter("@branchId",branchId),
            }; 

              DataTable dt = _dbHelper.Select(query,parameters); 

              foreach(DataRow row in dt.Rows)
            {
                  var customer = new ClsCustomers
                  {
                      CustomerId =Convert.ToInt32(row[0]),
                      CustomerName = row[1].ToString(),
                      Phone = row[2].ToString(),
                      Email = row[3].ToString(),
                      Address = row[4].ToString()   
                  };

                  customers.Add(customer); 
            }


           return customers; 
        }


       public ClsCustomers GetCustomerById(int companyId,int branchId,int customerId)
        {
            ClsCustomers customer = null; 
             
            string query = @"SELECT CustomerId,CustomerName,Phone,Email,Address
                             FROM Customers
                             WHERE CompanyId =@companyId
                             AND BranchId = @branchId
                             AND CustomerId = @customerId";

           SqlParameter[] parameters =
            {
                new SqlParameter("@companyId",companyId),
                new SqlParameter("@branchId",branchId),
                new SqlParameter("@customerId",customerId)
            }; 

              DataTable dt = _dbHelper.Select(query,parameters); 

              if(dt.Rows.Count > 0)
            {
                  var row = dt.Rows[0];
                  customer = new ClsCustomers
                  {
                      CustomerId =Convert.ToInt32(row[0]),
                      CustomerName = row[1].ToString(),
                      Phone = row[2].ToString(),
                      Email = row[3].ToString(),
                      Address = row[4].ToString()   
                  };
            }
            return customer;

        }


       public bool AddCustomer()
        {
            return false; 
        }

       public bool EditCustomer(ClsCustomers customer)
        {
            return false; 
        }

       public bool DeleteCustomer(ClsCustomers customer)
        {
            return false; 
        }

       



    }
}
    