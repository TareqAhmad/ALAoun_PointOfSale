using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.interfaces 
    
{
    public interface ISalesReturnsService
    {

        public List<SalesInvoiceVM> GetAllSalesInvoices(int companyId, int branchId);

        public SalesInvoiceVM GetSalesInvoice(int companyId, int branchId,int id);

        public bool AddSalesInvoice(int companyId, int branchId,ClsSalesInvoices salesInvoices);

        public bool EditSalesInvoice(int companyId, int branchId,ClsSalesInvoices salesInvoices);

        public bool DeleteSalesInvoice(int companyId, int branchId,ClsSalesInvoices salesInvoices);
    }

}