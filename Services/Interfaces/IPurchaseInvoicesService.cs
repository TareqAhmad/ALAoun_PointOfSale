using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces 
    
{
    public interface IPurchaseInvoicesService
    {

        public List<PurchaseInvoicesVM> GetAllPurchaseInvoices(int companyId, int branchId);

        public PurchaseInvoicesVM GetPurchaseInvoice(int companyId, int branchId,int id);

        public bool AddPurchaseInvoice(int companyId, int branchId,ClsPurchaseInvoices purchaseInvoices);

        public bool EditPurchaseInvoice(int companyId, int branchId,ClsPurchaseInvoices purchaseInvoices);

        public bool DeletePurchaseInvoices(int companyId, int branchId,ClsPurchaseInvoices purchaseInvoices);
    }

}