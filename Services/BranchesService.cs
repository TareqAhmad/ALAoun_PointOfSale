
using ALAoun_Pos.Models;
using ALAoun_Pos.Data; 
using ALAoun_Pos.Services.interfaces;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using System.Data;


namespace ALAoun_Pos.Services
{
    public class BranchesService(DbHelper dbHelper) : IBranchesService
    {

        public List<ClsBranches> GetAllBranches(int companyId)
        {
            var branches = new List<ClsBranches>();
               
             string query = @"SELECT branchId,branchName,phone,email,companyId,address,userBranch
                            FROM Branches 
                            WHERE CompanyId = @CompanyId; "; 


            SqlParameter[] parameters =
            {
                 new SqlParameter("@CompanyId",companyId)
            }; 

            DataTable dt = dbHelper.Select(query,parameters); 
          
            foreach(DataRow row in dt.Rows)
            {
                var branch = new ClsBranches{
                    BranchId = Convert.ToInt32(row[0]),
                    BranchName = row[1].ToString(),
                    Phone = row[2].ToString(),
                    Email = row[3].ToString(),
                    CompanyId = Convert.ToInt32(row[4]),
                    Address = row[5].ToString(),
                    userBranch = row[6].ToString()
                }; 

                branches.Add(branch);  
            }                 
             
            return branches;
        }

        public ClsBranches GetBranchById(int companyId,int branchId)
        {
            ClsBranches branch = null;

           
            return branch;
        }

        public bool AddBranch(ClsBranches branch)
        {
            return false; 
        }

        public bool EditBranch(ClsBranches branch)
        {
            return false; 
        }

        public bool DeleteBranch(ClsBranches branch)
        {
            return false; 
        }
    
    }

}
