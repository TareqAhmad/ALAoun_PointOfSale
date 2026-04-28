using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    public interface IPosPointsService
    {
        
       public List<ClsPosPoints> GetAllPosPoints(int CompanyId,int BranchId); 

       public ClsPosPoints GetPosPointById(int CompanyId,int BranchId,int PosId); 

        public bool AddPosPoint(ClsPosPoints branch);

        public bool EditPosPoint(ClsPosPoints branch);

        public bool DeletePosPoint(ClsPosPoints branch);

    }
}