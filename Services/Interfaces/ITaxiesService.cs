
using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    public interface ITaxiesService
    {
      
     public List<ClsTaxies> GetAllTaxies(int companyId);
     public ClsTaxies GetTaxById(int companyId,int id); 
     public bool AddTax(ClsTaxies tax);
     public bool EditTax(ClsTaxies tax);
     public bool DeleteTax(ClsTaxies tax);


    }
}