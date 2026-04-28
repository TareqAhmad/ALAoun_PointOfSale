
using ALAoun_Pos.Models;
using ALAoun_Pos.Data; 

using ALAoun_Pos.Services.interfaces;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ALAoun_Pos.Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly DbHelper? _dbHelper; 
       

        public CompaniesService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper; 
        }

        public List<ClsCompanies> GetAllCompanies()
        {
            List<ClsCompanies> companies = new List<ClsCompanies>();

            return companies;
        }

        public ClsCompanies GetCompanyById(int id)
        {
            ClsCompanies company = null;
    
            return company;
        }

         public ClsCompanies GetCompanyByUserCompany(string userCompany)
        {
            ClsCompanies Company = new ClsCompanies(); 
                  
            string query = @"SELECT CompanyId,CompanyName,TaxNumber,ClientId,SecretKey,
                                    Phone,Email,Address
                             FROM Companies
                             WHERE userCompany = @userCompany";
            SqlParameter[] parameters =
            {
                new SqlParameter("@userCompany",userCompany)
            };                

             DataTable  dt = _dbHelper.Select(query,parameters); 
         
             if(dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0]; 

                Company.CompanyId = Convert.ToInt32(row[0]); 
                Company.CompanyName = row[1].ToString(); 
                Company.TaxNumber = row[2].ToString(); 
                Company.ClientId = row[3].ToString(); 
                Company.SecretKey = row[4].ToString(); 
                Company.Phone = row[5].ToString(); 
                Company.Email = row[6].ToString(); 
                Company.Address = row[7].ToString(); 

            }

            return Company; 
        }


        public void AddCompany(ClsCompanies company)
        {
            
           
         }
       

        public bool EditCompany(ClsCompanies company)
        
        {
            return false;
       }

        public bool DeleteCompany(ClsCompanies company)
        
        {
            return false;
       }

    }
}