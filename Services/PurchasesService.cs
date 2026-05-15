using Microsoft.Data.SqlClient; 
using ALAoun_Pos.Data; 
using ALAoun_Pos.Services.interfaces;
using ALAoun_Pos.Models;
using System.Data;

namespace ALAoun_Pos.Services
{
    
    public class PurchasesService : IPurchasesService
    {
        private readonly DbHelper _dbHelper; 

        public PurchasesService(DbHelper dbHelper)
        {
             _dbHelper = dbHelper; 
        }

         public List<PurchaseInvoicesVM> GetAllPurchaseInvoices(int companyId, int branchId)
        {
            List<PurchaseInvoicesVM> purchaseInvoices = new List<PurchaseInvoicesVM>(); 

            string query=@"SELECT P.PurchaseId,P.PurchaseDate,Ty.typeDescription,P.PurchaseTotal,P.DiscountAmount,P.TaxAmount,P.NetAmount,S.SupplierName,M.PaymentType
                           FROM PurchaseInvoices P JOIN InvoicesTypes Ty ON P.InvoiceTypeId = Ty.InvoiceTypeId
                                                   JOIN Suppliers S ON P.SupplierId = S.SupplierId 
                                                   JOIN PaymentMethods M ON P.PaymentId = M.PaymentId

                            WHERE S.CompanyId = @companyId
                            AND S.BranchId = @branchId; ; "; 

                SqlParameter[] parameters =
            {
                new SqlParameter("@companyId",companyId),
                new SqlParameter("@branchId",branchId)
            };

            DataTable dt = _dbHelper.Select(query,parameters); 

            foreach(DataRow row in dt.Rows)
            {
                var purchaseInvoice = new PurchaseInvoicesVM
                {
                     PurchaseId     = Convert.ToInt32(row[0]),
                     PurchaseDate   = Convert.ToDateTime(row[1]),
                     InvoiceType    = row[2].ToString(),
                     PurchaseTotal  = Convert.ToDecimal(row[3]),
                     DiscountAmount = Convert.ToDecimal(row[4]),
                     TaxAmount      = Convert.ToDecimal(row[5]),
                     NetAmount      = Convert.ToDecimal(row[6]),
                     SupplierName   = row[7].ToString(),
                     PaymentType    = row[8].ToString()

                };

                purchaseInvoices.Add(purchaseInvoice); 
            }                                 


            return purchaseInvoices; 
        }

        public PurchaseInvoicesVM GetPurchaseInvoice(int companyId, int branchId,int id)
        {
            return new PurchaseInvoicesVM(); 
        }

        public bool AddPurchaseInvoice(InvoiceDto purchaseInvoicesDto)
        {
            return false; 
        }

        public bool EditPurchaseInvoice(int companyId, int branchId,ClsPurchases purchaseInvoices)
        {
            return false; 
        }

        public bool DeletePurchaseInvoices(int companyId, int branchId,ClsPurchases purchaseInvoices)
        {
            return false; 
        }

    }
}

