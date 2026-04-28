using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces 
    
{
    public interface ISalesInvoicesService
    {

        public List<SalesInvoiceVM> GetAllSalesInvoices(int companyId, int branchId,int posId);

        public SalesInvoiceVM GetSalesInvoice(int companyId, int branchId,int posId,int id);

        public bool AddSalesInvoice(int companyId, int branchId,int posId,int userId,SalesInvoiceDto salesInvoiceDto);

        public bool EditSalesInvoice(int companyId, int branchId,SalesInvoiceDto salesInvoiceDto);

        public bool DeleteSalesInvoice(int companyId, int branchId,SalesInvoiceDto salesInvoiceDto);
    }

}