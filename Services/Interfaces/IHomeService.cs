using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    
    public interface IHomeService
    {

        public ClsUserInfo Login(int companyId, int branchId,int posId,int userId,string userPassword);

        public void Logout(ClsCompanies company, ClsBranches branch,ClsPosPoints pos,ClsUsers user); 
    }
}
    