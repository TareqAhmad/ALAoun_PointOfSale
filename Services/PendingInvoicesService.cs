using System;
using Microsoft.Data.SqlClient; 
using ALAoun_Pos.Data; 
using ALAoun_Pos.Services.interfaces;
using ALAoun_Pos.Models;
using System.Data;
using System.Text.Json;


namespace ALAoun_Pos.Services
{
    
    public class PendingInvoicesService : IPendingInvoicesService
    {
        private readonly DbHelper _dbHelper; 
        private readonly IConfiguration _configuration;
        private readonly IProductsService _productService; 



        public PendingInvoicesService(DbHelper dbHelper, IConfiguration configuration, IProductsService productService)
        {
             _dbHelper = dbHelper; 
             _configuration = configuration;
             _productService = productService;
        }

         public List<ClsPendingInvoices> GetAllPendingInvoices(int companyId, int branchId,int posId) 
        { 
            List<ClsPendingInvoices> pendingInvoices = new List<ClsPendingInvoices>(); 

            string query = "SELECT * FROM PendingInvoices WHERE companyId = @companyId AND branchId = @branchId";

            SqlParameter[] parameters = 
            {
                new SqlParameter("@companyId", companyId),
                new SqlParameter("@branchId", branchId)
            };

                var dataTable = _dbHelper.Select(query, parameters); 
    
                foreach (DataRow row in dataTable.Rows)
                {
                    ClsPendingInvoices pendingInvoice = new ClsPendingInvoices
                    {
                        Status = Convert.ToBoolean(row["Status"]),
                        PendingInvoiceId = Convert.ToInt32(row["PendingInvoiceId"]),
                        CustomerId = Convert.ToInt32(row["customerId"]),
                        TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                        NetAmount = Convert.ToDecimal(row["NetAmount"]),
                        InvoiceDate = Convert.ToDateTime(row["invoiceDate"])
                    };
    
                    pendingInvoices.Add(pendingInvoice);
                }
    
                return pendingInvoices; 

            
        }

        public PendingInvoiceViewModel GetPendingInvoiceById(int companyId, int branchId,int posId,int id)
        {


            
            PendingInvoiceViewModel pendingInvoice = null; 

            SqlParameter[] parameters = 
            {
                new SqlParameter("@companyId", companyId),
                new SqlParameter("@branchId", branchId),
                new SqlParameter("@posId", posId),
                new SqlParameter("@pendingId", id)            
            };

          
     
             using(SqlDataReader reader = _dbHelper.ExecuteReaderWithStoredProcedure("GetPendingInvoiceWithItems", parameters))
            {
                 
                // ===========
                // Header
                // ===========
                if (!reader.Read())
                {
                    return null;
                }

                pendingInvoice = new PendingInvoiceViewModel
                {
                    PendingInvoiceId = reader["PendingInvoiceId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PendingInvoiceId"]),
                    InvoiceDate =  reader["InvoiceDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["InvoiceDate"]),
                    CustomerId = reader["customerId"] == DBNull.Value ? 0 : reader.GetInt32(reader.GetOrdinal("customerId")),
                    QuantityItems = reader["QuantityItems"] == DBNull.Value ? 0 : reader.GetInt32(reader.GetOrdinal("QuantityItems")),
                    InvoiceType = reader["InvoiceType"] == DBNull.Value ? "" : reader["InvoiceType"].ToString(),
                    PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? "" : reader["PaymentMethod"].ToString(),
                    Items = new List<CartItemsViewModel>()
                };

                // ===========
                // Items
                // ===========
                if(reader.NextResult())
                {
                        while(reader.Read())
                    {
                            pendingInvoice.Items.Add(new CartItemsViewModel
                            {
                                itemId = reader["itemId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["itemId"]),
                                productId = reader["productId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["productId"]),
                                productName = reader["ProductName"] == DBNull.Value ? "" :  reader["ProductName"].ToString(),
                                quantity = reader["quantity"] == DBNull.Value ? 0 : Convert.ToInt32(reader["quantity"]),
                                discountRate = reader["discountRate"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["discountRate"]),
                                taxRate = reader["taxRate"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["taxRate"]),
                                unitPrice = reader["unitPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["unitPrice"]),   
                                totalPrice = reader["totalPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["totalPrice"])
                            });
                        }
                }
       }

            return pendingInvoice;

          
        }


        public bool AddPendingInvoice(int companyId, int branchId,int posId,int userId,PendingInvoiceDto pendingInvoiceDto)
        {
        

           if(pendingInvoiceDto == null)
            {
                throw new Exception("بيانات الفاتورة غير صحيحة");
            }
            
            var  items      = pendingInvoiceDto.Items; 
           
            using(SqlConnection conn= new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                using(SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string getQuery = "SELECT dbo.GetFirstEmptyAfterOccupied(@companyId, @branchId, @posId)";

                        SqlParameter [] getParameters =
                        {
                                new SqlParameter("@companyId",companyId),
                                new SqlParameter("@branchId",branchId),
                                new SqlParameter("@posId",posId),
                        };

                       // تنفيذ جلب الـ ID - نستخدم Transaction لضمان ثبات البيانات
                        var result = _dbHelper.ExecuteScalarWithoutStoredProcedure(getQuery, getParameters, transaction);
                        int targetId = result != null ? Convert.ToInt32(result) : 0;

                        if (targetId <= 0)throw new Exception("لا توجد طاولات فارغة متاحة حالياً.");
                        

                        string  updateQuery = @"UPDATE PendingInvoices
                                                SET status = @status,
                                                invoiceDate = @sysDate,
                                                customerId = @customerId,
                                                TotalAmount = @sumAmount,
                                                NetAmount = @netAmount,
                                                DiscountId =@discountId,
                                                TaxId = @taxId,
                                                QuantityItems = @countItems,
                                                InvoiceTypeId = @InvoiceTypeId,
                                                paymentId = @paymentId,
                                                companyId = @companyId,
                                                branchId = @branchId,
                                                userId = @userId,
                                                posId = @posId,
                                                notes = @notes
                                                OUTPUT INSERTED.PendingInvoiceId
                                                WHERE pendingInvoiceId = @targetId
                                                AND companyId = @companyId
                                                AND branchId = @branchId
                                                AND posId = @posId"; 



                            SqlParameter[] updateParameters =
                            {
                                new SqlParameter("@status",1),
                                new SqlParameter("@sysDate",DateTime.Now),
                                new SqlParameter("@customerId",pendingInvoiceDto.customerId),
                                new SqlParameter("@sumAmount",pendingInvoiceDto.sumAmount),
                                new SqlParameter("@netAmount",pendingInvoiceDto.netInvoice),
                                new SqlParameter("@discountId",DBNull.Value),
                                new SqlParameter("@taxId",DBNull.Value),
                                new SqlParameter("@countItems",items.Count),
                                new SqlParameter("@InvoiceTypeId",1),
                                new SqlParameter("@paymentId",DBNull.Value),
                                new SqlParameter("@targetId",targetId),
                                new SqlParameter("@companyId",companyId),
                                new SqlParameter("@branchId",branchId),
                                new SqlParameter("@posId",posId),
                                new SqlParameter("@userId",userId),
                                new SqlParameter("@notes",DBNull.Value)

                            } ;   


                             int  pendingInvoiceId = _dbHelper.ExecuteScalarWithoutStoredProcedure(updateQuery,updateParameters,transaction) ;                
             
                             if(pendingInvoiceId > 0)
                            {
                                string deleteQuery = @"DELETE FROM PendingInvoiceItems
                                                        WHERE PendingInvoiceId = @pendingInvoiceId";
                                _dbHelper.Execute(deleteQuery, new [] { new SqlParameter("@pendingInvoiceId", pendingInvoiceId) }, transaction);
                                
                                foreach(var item in items)
                                {
                                     var product = _productService.GetProductById(companyId,branchId,item.productId);
                                     if(product == null) throw new Exception("المنتج غير موجود");
                                     decimal totalRow = product.ProductPrice * item.quantity; 

                                    string  insertQuery = @"INSERT PendingInvoiceItems (PendingInvoiceId,ProductId,Quantity,DiscountId,TaxId,UnitPrice,TotalPrice)
                                                     VALUES (@pendingInvoiceId, @productId,@quantity,@discountId,@taxId,@unitPrice,@totalPrice)";
                                     SqlParameter[] insertParameters =
                                    {
                                            new SqlParameter("@PendingInvoiceId",pendingInvoiceId),
                                            new SqlParameter("@productId",item.productId),
                                            new SqlParameter("@quantity",item.quantity),
                                            new SqlParameter("@discountId",DBNull.Value),
                                            new SqlParameter("@taxId",DBNull.Value),
                                            new SqlParameter("@unitPrice",product.ProductPrice),
                                            new SqlParameter("@totalPrice",totalRow)

                                    };  
                                
                                  _dbHelper.Execute(insertQuery,insertParameters,transaction) ;
                                
                                }
                                transaction.Commit();
                                return true;

                            }
                            return false;

                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("حدث خطأ أثناء حفظ الفاتورة المؤقتة: " + ex.Message);
                    }
                }
            }
        }
      
        public bool EditPendingInvoice(int companyId, int branchId,ClsPendingInvoices purchaseInvoices)
        {
            return false; 
        }

        public bool DeletePendingInvoice(int companyId, int branchId,ClsPendingInvoices purchaseInvoices)
        {
            return false; 
        }

    }
}

