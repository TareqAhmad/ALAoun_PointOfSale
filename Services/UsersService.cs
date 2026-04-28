using System.Data;
using Microsoft.Data.SqlClient;
using ALAoun_Pos.Models; 
using ALAoun_Pos.Data; 
using ALAoun_Pos.Services.interfaces;


namespace ALAoun_Pos.Services
{
    
     public class UsersService : IUsersServices
    {

          private readonly DbHelper _dbHelper;

            public UsersService(DbHelper dbHelper)
            {
                _dbHelper = dbHelper;
            }
        public List<ClsUsers> GetAllUsers(int companyId,int branchId)
        {

            List<ClsUsers> users  = new List<ClsUsers>(); 

            string query = @"SELECT UserId,UserName,PasswordHash,Phone,Email,RoleId
                            FROM Users
                            WHERE companyId = @CompanyId
                            AND branchId = @BranchId;"; 
               
            SqlParameter[] parameters =
            {
                new SqlParameter("@CompanyId", companyId),
                new SqlParameter( "@BranchId" , branchId)
                
            };

            DataTable dt = _dbHelper.Select(query,parameters); 

            foreach(DataRow row in dt.Rows)
            {
                var user = new ClsUsers
                {
                    UserId = Convert.ToInt32(row[0]),
                    UserName = row[1].ToString(),
                    Password = row[2].ToString(),
                    Phone = row[3].ToString(),
                    Email = row[4].ToString(),
                    RoleId =Convert.ToInt32(row[5])
                }; 

                users.Add(user); 
            }

            return users ; 

        }


        public ClsUsers GetUserById(ClsUsers user)
        {
             ClsUsers _user = null; 
             return _user; 
        }
    public UserRolePrivilegesVM GetUserRolePrivilegesById(int companyId, int branchId, int userId)
            {
                 var vm = new UserRolePrivilegesVM{UserId = userId, Privileges = new List<ClsPrivileges>()};
    
                    string sql = @"
                                SELECT u.RoleId, p.Priv_Id, p.Priv_Name, p.description
                                FROM Users u
                                INNER JOIN RolePrivileges rp ON u.RoleId = rp.Role_Id
                                INNER JOIN Privileges p ON rp.Priv_Id = p.Priv_Id
                                WHERE u.UserId = @UserId
                                AND u.CompanyId = @CompanyId
                                AND u.BranchId = @BranchId;";
    
                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@CompanyId", companyId),
                    new SqlParameter("@BranchId", branchId)
                };
    
                DataTable dt = _dbHelper.Select(sql, parameters);
    
                foreach (DataRow row in dt.Rows)
                {
                    vm.RoleId = Convert.ToInt32(row[0]);
                     vm.Privileges.Add( new ClsPrivileges
                    {
                     
                        PrivilegeId = Convert.ToInt32(row["Priv_Id"]),
                        PrivilegeName = row["Priv_Name"].ToString(),
                        Description = row["description"].ToString()
                    });
    
                  
                }
    
                return vm;
            }

        public bool AddUser(ClsUsers user){
            return false; 
        }

        public bool EditUser(ClsUsers user)
        {
            return false; 
        }

        public bool DeleteUser(ClsUsers user)
        {
            return false; 
        }
     
    }
}
