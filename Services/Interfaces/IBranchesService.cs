using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    public interface IBranchesService
    {
        public List<ClsBranches> GetAllBranches(int companyId);

        public ClsBranches GetBranchById(int companyId,int branchId);

        public bool AddBranch(ClsBranches branch);

        public bool EditBranch(ClsBranches branch);

        public bool DeleteBranch(ClsBranches branch);
    }
}