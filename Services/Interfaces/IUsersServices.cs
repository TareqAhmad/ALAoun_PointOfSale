
using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    
    public interface IUsersServices
    {
       

          public List<ClsUsers> GetAllUsers(int companyId,int branchId);
          
          public ClsUsers GetUserById(ClsUsers user);

          public UserRolePrivilegesVM GetUserRolePrivilegesById(int companyId,int branchId,int userId);

          public bool AddUser(ClsUsers user);

          public bool EditUser(ClsUsers user);

          public bool DeleteUser(ClsUsers user);





    }

}