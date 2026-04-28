using Microsoft.Data.SqlClient; 
using ALAoun_Pos.Services.interfaces;
using ALAoun_Pos.Models; 
using ALAoun_Pos.Data;
using System.Data;

namespace ALAoun_Pos.Services
{
      
   public class SalesInvoicesService : ISalesInvoicesService
    {


         private readonly DbHelper _dbHelper; 
         private readonly IConfiguration _configuration;

        public SalesInvoicesService(DbHelper dbHelper,IConfiguration configuration)
        {
            _dbHelper = dbHelper;
            _configuration = configuration;
        }

        public List<SalesInvoiceVM> GetAllSalesInvoices(int companyId, int branchId,int posId) {
                 List<SalesInvoiceVM> salesInvoices = new List<SalesInvoiceVM>();

       
                 string query = @"SELECT S.InvoiceId,S.InvoiceDate,Ty.typeDescription , P.PaymentType,
                                         C.CustomerName,S.QuantityItems, D.DiscountRate,
                                          T.TaxRate,S.TotalAmount , S.NetAmount,U.UserName
                                 FROM SalesInvoices S 
                                 LEFT JOIN InvoicesTypes Ty ON S.InvoiceTypeId = Ty.InvoiceTypeId
                                 LEFT JOIN Customers C ON S.CustomerId = C.CustomerId 
                                 LEFT JOIN Discounts D ON S.DiscountId = D.DiscountId
                                 LEFT JOIN  Taxies T ON S.TaxId = T.TaxId 
                                 LEFT JOIN PaymentMethods P ON S.PaymentId = P.PaymentId
                                 LEFT JOIN Users U ON S.UserId = U.UserId
                                WHERE S.CompanyId = @companyId
                                AND S.BranchId = @branchId
                                AND S.PosId = @posId;"; // Adjust the table name and columns as needed
            
                 SqlParameter[] parameters =
                {
                    new SqlParameter("@companyId",companyId),
                    new SqlParameter("@branchId",branchId),
                    new SqlParameter("@posId",posId),
                }; 

                DataTable dt = _dbHelper.Select(query,parameters); 

                foreach(DataRow row in dt.Rows)
                {
                     var saleInvoice = new SalesInvoiceVM
                     {
                         InvoiceId      = row["InvoiceId"] != DBNull.Value ? Convert.ToInt32(row["InvoiceId"]) : 0,
                         InvoiceDate    = row["InvoiceDate"] != DBNull.Value ? Convert.ToDateTime(row["InvoiceDate"]) : DateTime.MinValue,
                         InvoiceType    = row["typeDescription"] != DBNull.Value ? row["typeDescription"].ToString() :"عام",
                         PaymentType    = row["PaymentType"] != DBNull.Value ? row["PaymentType"].ToString() : "كاش",
                         CustomerName   = row["CustomerName"] != DBNull.Value ? row["CustomerName"].ToString() : "زبون عام",
                         QuantityItems  = row["QuantityItems"] != DBNull.Value ? Convert.ToInt32(row["QuantityItems"]) : 0,
                         DiscountRate   = row["DiscountRate"] != DBNull.Value ? Convert.ToDecimal(row["DiscountRate"]) : 0,
                         TaxRate        = row["TaxRate"] != DBNull.Value ? Convert.ToDecimal(row["TaxRate"]) : 0,
                         TotalAmount    = row["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(row["TotalAmount"]) : 0,
                         NetAmount      = row["NetAmount"] != DBNull.Value ? Convert.ToDecimal(row["NetAmount"]) : 0,
                         userName    = row["UserName"] != DBNull.Value ? row["UserName"].ToString() : "النظام"
                   
                     }; 

                     salesInvoices.Add(saleInvoice);
                }
               
                return salesInvoices;
          
        }

        public SalesInvoiceVM GetSalesInvoice(int companyId, int branchId,int posId,int id)
        {
            return new SalesInvoiceVM();
        }

        public bool AddSalesInvoice(int companyId, int branchId, int posId, int userId, SalesInvoiceDto salesInvoiceDto)
        {
            var items = salesInvoiceDto.Items;
            decimal totalAmount = items.Sum(i => i.UnitPrice * i.quantity);
            decimal netAmount = totalAmount;

            // الحصول على نص الاتصال من DBHelper (أو من Configuration مباشرة)
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                // بدء العملية المركبة
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = @"INSERT INTO SalesInvoices (CompanyId, BranchId, PosId, UserId, CustomerId, InvoiceTypeId, DiscountId, TaxId, PaymentId, QuantityItems, TotalAmount, NetAmount)
                                        VALUES (@companyId, @branchId, @posId, @userId, @customerId, @invoiceTypeId, @discountId, @taxId, @paymentId, @quantityItems, @totalAmount, @netAmount)
                                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        SqlParameter[] parameters = {
                            new SqlParameter("@companyId", companyId),
                            new SqlParameter("@branchId", branchId),
                            new SqlParameter("@posId", posId),
                            new SqlParameter("@userId", userId),
                            new SqlParameter("@customerId", salesInvoiceDto.customerId),
                            new SqlParameter("@invoiceTypeId", salesInvoiceDto.InvoiceTypeId),
                            new SqlParameter("@discountId", DBNull.Value),
                            new SqlParameter("@taxId", DBNull.Value),
                            new SqlParameter("@paymentId", salesInvoiceDto.PaymentId),
                            new SqlParameter("@quantityItems", items.Count),
                            new SqlParameter("@totalAmount", totalAmount),
                            new SqlParameter("@netAmount", netAmount)
                        };

                        // تنفيذ جلب الـ ID باستخدام الترانزاكشن
                        int invoiceId = _dbHelper.ExecuteScalarWithoutStoredProcedure(query, parameters, transaction);

                        if (invoiceId > 0)
                        {
                            // 2. حلقة إدخال الأصناف
                            foreach (var item in items)
                            {
                                string itemQuery = @"INSERT INTO SalesInvoiceItems (invoiceId, ProductId, Quantity, DiscountId, TaxId, UnitPrice, TotalPrice)
                                                    VALUES (@invoiceId, @productId, @quantity, @discountId, @taxId, @unitPrice, @totalPrice);";

                                SqlParameter[] itemParameters = {
                                    new SqlParameter("@invoiceId", invoiceId),
                                    new SqlParameter("@productId", item.productId),
                                    new SqlParameter("@quantity", item.quantity),
                                    new SqlParameter("@discountId", DBNull.Value),
                                    new SqlParameter("@taxId", DBNull.Value),
                                    new SqlParameter("@unitPrice", item.UnitPrice),
                                    new SqlParameter("@totalPrice", item.UnitPrice * item.quantity)
                                };

                                // تنفيذ إدخال الصنف تحت نفس الترانزاكشن
                                _dbHelper.Execute(itemQuery, itemParameters, transaction);
                            }

                            // إذا وصلنا هنا بنجاح، نعتمد التغييرات
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            // فشل في جلب الـ ID
                            transaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        // في حال حدوث أي خطأ (SQL أو Code)، تراجع عن كل شيء
                        transaction.Rollback();
                        // يفضل تسجيل الخطأ هنا log.Error(ex);
                        return false;
                    }
                }
            }
        }
        public bool EditSalesInvoice(int companyId, int branchId,SalesInvoiceDto salesInvoiceDto)
        {
            return false; 
        }

        public bool DeleteSalesInvoice(int companyId, int branchId,SalesInvoiceDto salesInvoiceDto)
        {
            return false; 
        }
    }


}