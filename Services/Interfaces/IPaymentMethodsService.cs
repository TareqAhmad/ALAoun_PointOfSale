

using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces
{
    public interface IPaymentMethodsService
    {
    
        public List<ClsPaymentMethods> GetAllPaymentMethods(int companyId, int branchId);
        public ClsPaymentMethods GetPaymentMethodById(int companyId, int branchId, int id);   
        public bool AddPaymentMethod(ClsPaymentMethods paymentMethod);
        public bool EditPaymentMethod(ClsPaymentMethods paymentMethod);
        public bool DeletePaymentMethod(ClsPaymentMethods paymentMethod);
    }
}