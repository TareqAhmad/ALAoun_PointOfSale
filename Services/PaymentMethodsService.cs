using System.Data;
using ALAoun_Pos.Data;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;
using Microsoft.Data.SqlClient;

namespace ALAoun_Pos.Services
{
    public class PaymentMethodsService : IPaymentMethodsService
    {
        private readonly DbHelper _dbHelper;

        public PaymentMethodsService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<ClsPaymentMethods> GetAllPaymentMethods(int companyId, int branchId)
        {
            List<ClsPaymentMethods> paymentMethods = new List<ClsPaymentMethods>(); 
            string query = @"SELECT paymentId,paymentType,description
                             FROM PaymentMethods 
                             WHERE CompanyId = @CompanyId 
                             AND BranchId = @BranchId
                             AND IsActive = 1";

            SqlParameter[] parameters =
            {
                new SqlParameter("@CompanyId", companyId),
                new SqlParameter("@BranchId", branchId) 
            }; 
            DataTable dt = _dbHelper.Select(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                ClsPaymentMethods paymentMethod = new ClsPaymentMethods
                {
                    PaymentId = Convert.ToInt32(row[0]),
                    PaymentType = row[1].ToString(),
                    Description = row[2].ToString(),
                };
                paymentMethods.Add(paymentMethod);
            }
            return paymentMethods;
        }
        public ClsPaymentMethods GetPaymentMethodById(int companyId, int branchId, int id)
        {
            ClsPaymentMethods paymentMethod =null; 
            string query = @"SELECT * 
                             FROM PaymentMethods 
                             WHERE CompanyId = @CompanyId 
                             AND BranchId = @BranchId
                             AND PaymentId = @PaymentId
                             AND ISactive = 1";

            SqlParameter[] parameters =
            {
                new SqlParameter("@CompanyId", companyId),
                new SqlParameter("@BranchId", branchId),        
                new SqlParameter("@PaymentId", id)
            };
            DataTable dt = _dbHelper.Select(query, parameters);     

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                paymentMethod = new ClsPaymentMethods
                {
                    PaymentId = Convert.ToInt32(row[1]),
                    PaymentType = row[2].ToString(),
                    Description = row[3].ToString(),
                };
            }
            return paymentMethod;
        }
        public bool AddPaymentMethod(ClsPaymentMethods paymentMethod)
        {
            return false;
        }
        public bool EditPaymentMethod(ClsPaymentMethods paymentMethod)
        {
            return false;
        }
        public bool DeletePaymentMethod(ClsPaymentMethods paymentMethod)
        {
            return false;
        }
    }
}


