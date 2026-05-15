using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces 
    
{
    public interface IPurchasesService
    {

        public List<PurchaseInvoicesVM> GetAllPurchaseInvoices(int companyId, int branchId);

        public PurchaseInvoicesVM GetPurchaseInvoice(int companyId, int branchId,int id);

        public bool AddPurchaseInvoice(InvoiceDto purchaseInvoicesDto);

        public bool EditPurchaseInvoice(int companyId, int branchId,ClsPurchases purchaseInvoices);

        public bool DeletePurchaseInvoices(int companyId, int branchId,ClsPurchases purchaseInvoices);
    }

}