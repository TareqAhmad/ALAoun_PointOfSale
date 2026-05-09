using Microsoft.Data.SqlClient;
using System.Data;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;
using ALAoun_Pos.Data; 

namespace ALAoun_Pos.Services
{
    
    public class SuppliersService : ISuppliersService
    {
        
         private readonly DbHelper _dbHelper; 

           public SuppliersService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }


        public List<ClsSuppliers> GetAllSuppliers(int companyId,int branchId)
        {
                  List<ClsSuppliers> suppliers  = new List<ClsSuppliers>();
             
            string query = @"SELECT SupplierId,SupplierName,Phone,Email,Address
                             FROM Suppliers
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
                  var supplier = new ClsSuppliers
                  {
                      SupplierId =Convert.ToInt32(row[0]),
                      SupplierName = row[1].ToString(),
                      Phone = row[2].ToString(),
                      Email = row[3].ToString(),
                      Address = row[4].ToString()   
                  };

                  suppliers.Add(supplier); 
            }


           return suppliers; 
        } 


       public ClsSuppliers GetSupplierById(int companyId,int branchId,int Id)
        {
            ClsSuppliers supplier = null;

            string query = @"SELECT SupplierId,SupplierName,Phone,Email,Address
                             FROM Suppliers
                             WHERE CompanyId =@companyId
                             AND BranchId = @branchId
                             AND SupplierId = @supplierId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@companyId",companyId),
                new SqlParameter("@branchId",branchId),
                new SqlParameter("@supplierId",Id)
            };

            DataTable dt = _dbHelper.Select(query, parameters);

            if(dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                supplier = new ClsSuppliers
                {
                    SupplierId = Convert.ToInt32(row[0]),
                    SupplierName = row[1].ToString(),
                    Phone = row[2].ToString(),
                    Email = row[3].ToString(),
                    Address = row[4].ToString()
                };
            }
            return supplier;

        }


       public bool AddSupplier(SupplierDto supplierDto)
        {


                string query = @"INSERT INTO Suppliers (SupplierName,Phone,Email,Address,CompanyId,BranchId)
                                VALUES (@supplierName,@phone,@email,@address,@companyId,@branchId)";
    
                SqlParameter[] parameters =
                {
                    new SqlParameter("@supplierName",supplierDto.supplierName),
                    new SqlParameter("@phone",supplierDto.phone),
                    new SqlParameter("@email",supplierDto.email),
                    new SqlParameter("@address",supplierDto.address),
                    new SqlParameter("@companyId",supplierDto.companyId),
                    new SqlParameter("@branchId",supplierDto.branchId)
                };
    
                int rowsAffected = _dbHelper.Execute(query, parameters);
    
                return rowsAffected > 0;  
        }

       public bool EditSupplier(ClsSuppliers supplier)
        {
            return false; 
        }

       public bool DeleteSupplier(ClsSuppliers supplier)
        {
            return false; 
        } 

    }
}