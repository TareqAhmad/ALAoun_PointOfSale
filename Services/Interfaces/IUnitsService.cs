
using ALAoun_Pos.Models;


namespace ALAoun_Pos.Services.interfaces
{
    public interface IUnitsService
    {
      
     public List<ClsUnits> GetAllUnits(int companyId,int branchId);
     public ClsUnits GetUnitById(int companyId,int branchId,int unitId); 
     public bool AddUnit(string unitName,int companyId,int branchId);
     public bool EditUnit(ClsUnits unit);
     public bool DeleteUnit(ClsUnits unit);


    }
}