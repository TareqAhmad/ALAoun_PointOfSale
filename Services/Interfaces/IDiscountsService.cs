
using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    public interface IDiscountsService
    {
      
     public List<ClsDiscounts> GetAllDiscounts(int companyId, int branchId);
     public ClsDiscounts GetDiscountById(int companyId, int branchId,int id); 
     public bool AddDiscount(ClsDiscounts discount);
     public bool EditDiscount(ClsDiscounts discount);
     public bool DeleteDiscount(ClsDiscounts discount);


    }
}