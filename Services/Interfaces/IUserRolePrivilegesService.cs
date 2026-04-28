
namespace ALAoun_Pos.Models
{
    public interface IUserRolePrivilegesService
    {
        public UserRolePrivilegesVM GetUserRolePrivilegesById(int companyId,int branchId,int userId);

       
    }
    
}