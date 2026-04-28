using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces 
    
{
    public interface IPendingInvoicesService
    {

        public List<ClsPendingInvoices> GetAllPendingInvoices(int companyId, int branchId,int posId);

        public PendingInvoiceViewModel GetPendingInvoiceById(int companyId, int branchId,int posId,int id);

        public bool AddPendingInvoice(int companyId, int branchId,int posId,int userId,PendingInvoiceDto pendingInvoiceDto);

        public bool EditPendingInvoice(int companyId, int branchId,ClsPendingInvoices purchaseInvoices);

        public bool DeletePendingInvoice(int companyId, int branchId,ClsPendingInvoices purchaseInvoices);
    }

}