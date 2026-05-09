using Microsoft.Data.SqlClient;
using System.Data;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;
using ALAoun_Pos.Data; 

namespace ALAoun_Pos.Services
{
    

  public class HomeService : IHomeService
  {
     
    private readonly DbHelper _dbHelper; 
     
    public HomeService(DbHelper dbHelper)
    {
       _dbHelper = dbHelper; 
    }

    public ClsUserInfo Login(int companyId, int branchId,int posId,int userId,string userPassword)
    {
            ClsUserInfo user = null;    
           string query = ""; 
     
              query  = @"SELECT U.userId,U.userName,U.RoleId,U.CompanyId,U.BranchId,U.PosId,C.companyName,C.isActive,B.BranchName,P.PosName
                            FROM Users U JOIN Companies C ON U.CompanyId = C.CompanyId
                                        JOIN Branches B ON U.BranchId = B.BranchId
                                        LEFT JOIN PosPoints P ON U.PosId = P.PosId
                            WHERE U.userId =@userId
                            AND U.passwordHash = @userPassword
                            AND U.companyId = @companyId
                            AND U.branchId = @branchId
                            AND  (U.RoleId = 1  OR IsNULL(U.posId, 0) = @posId)";
        
                           

          SqlParameter[] parameters =
          {
              new SqlParameter("@userId",userId),
              new SqlParameter("@userPassword",userPassword),
              new SqlParameter("@companyId",companyId),
              new SqlParameter("@branchId",branchId),
              new SqlParameter("@posId",posId)
                  
            
          };                

          DataTable dt = _dbHelper.Select(query,parameters);       
          
          if (dt.Rows.Count > 0)
          {   
                DataRow row =  dt.Rows[0]; 
                 user = new ClsUserInfo
                {
                  UserId = Convert.ToInt32(row[0]),
                  UserName = row[1].ToString(),
                  RoleId = Convert.ToInt32(row[2]),
                  CompanyId = Convert.ToInt32(row[3]),
                  BranchId = Convert.ToInt32(row[4]),
                  PosId  =  (row[5] == DBNull.Value) ? 0 : Convert.ToInt32(row[5]),
                  CompanyName = row[6].ToString(),
                  IsActive = Convert.ToBoolean(row[7]),
                  BranchName = row[8].ToString(),
                  PosName = row[9].ToString()
                }; 
          }
            
          
          return user; 
         }
    
    
    public void Logout(ClsCompanies company, ClsBranches branch,ClsPosPoints pos,ClsUsers user)
    {
        
    }
  
  
  }
}