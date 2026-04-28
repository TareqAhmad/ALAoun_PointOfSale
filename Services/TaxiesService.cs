
using Microsoft.Data.SqlClient;
using System.Data;
using ALAoun_Pos.Models;
using ALAoun_Pos.Data;
using ALAoun_Pos.Services.interfaces;      

namespace ALAoun_Pos.Services
{
    public class TaxiesService : ITaxiesService
    {
        private readonly DbHelper _dbHelper;
        public TaxiesService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
      public List<ClsTaxies> GetAllTaxies(int companyId)
       { 
           List<ClsTaxies> taxies = new List<ClsTaxies>(); 

           string query = @"SELECT taxId,taxName,taxRate,description
                           FROM Taxies
                           WHERE companyId =@companyId"; 

           SqlParameter[] parameters=
            {
                new SqlParameter("@companyId",companyId)
            }; 

            DataTable dt = _dbHelper.Select(query,parameters); 

            foreach(DataRow row in dt.Rows)
            {
                var tax = new ClsTaxies
                {
                    taxId = Convert.ToInt32(row[0]),
                    taxName = row[1].ToString(),
                    taxRate = Convert.ToDecimal(row[2]),
                    description = row[3].ToString(),        
           
                }; 

                taxies.Add(tax); 
            }

            return taxies; 
          
    }
     public ClsTaxies GetTaxById(int companyId,int id)
        {
            ClsTaxies tax = null;

            string query = @"SELECT taxId,taxName,taxRate,description
                           FROM Taxies
                           WHERE companyId =@companyId AND taxId=@taxId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@companyId", companyId),
                new SqlParameter("@taxId", id)
            };

            DataTable dt = _dbHelper.Select(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                tax = new ClsTaxies
                {
                    taxId = Convert.ToInt32(row[0]),
                    taxName = row[1].ToString(),
                    taxRate = Convert.ToDecimal(row[2]),
                    description = row[3].ToString(),

                };
            }

            return tax;
        } 
     public bool AddTax(ClsTaxies tax)
        {
            return false;
        }
     public bool EditTax(ClsTaxies tax)
        {
            return false;
        }
     public bool DeleteTax(ClsTaxies tax)
        {
            return false; 
        }


    }

}