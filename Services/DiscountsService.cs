
using Microsoft.Data.SqlClient;
using System.Data;
using ALAoun_Pos.Models;
using ALAoun_Pos.Data;
using ALAoun_Pos.Services.interfaces;      

namespace ALAoun_Pos.Services
{
    public class DiscountsService : IDiscountsService
    {
        private readonly DbHelper _dbHelper;
        public DiscountsService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
      public List<ClsDiscounts> GetAllDiscounts(int companyId, int branchId)
    {
        
           List<ClsDiscounts> discounts = new List<ClsDiscounts>(); 

           string query = @"SELECT discountId,discountName,discountType,discountRate,startDate,endDate
                           FROM Discounts
                           WHERE companyId =@companyId
                           AND branchId = @branchId"; 

           SqlParameter[] parameters=
            {
                new SqlParameter("@companyId",companyId),
                new SqlParameter("@branchId", branchId)
            }; 

            DataTable dt = _dbHelper.Select(query,parameters); 

            foreach(DataRow row in dt.Rows)
            {
                var discount = new ClsDiscounts
                {
                    discountId = Convert.ToInt32(row[0]),
                    discountName = row[1].ToString(),
                    discountType = row[2].ToString(),
                    discountRate = Convert.ToDecimal(row[3]),
                    startDate = Convert.ToDateTime(row[4]),
                    endDate = Convert.ToDateTime(row[5])
                }; 

                discounts.Add(discount); 
            }

            return discounts; 
          
    }
     public ClsDiscounts GetDiscountById(int companyId, int branchId,int id)
        {
            ClsDiscounts discount = null;

            string query = @"SELECT discountId,discountName,discountType,discountRate,startDate,endDate
                           FROM Discounts
                           WHERE companyId =@companyId
                           AND branchId = @branchId
                           AND discountId = @id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@companyId", companyId),
                new SqlParameter("@branchId", branchId),
                new SqlParameter("@id", id)
            };

            DataTable dt = _dbHelper.Select(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                discount = new ClsDiscounts
                {
                    discountId = Convert.ToInt32(row[0]),
                    discountName = row[1].ToString(),
                    discountType = row[2].ToString(),
                    discountRate = Convert.ToDecimal(row[3]),
                    startDate = Convert.ToDateTime(row[4]),
                    endDate = Convert.ToDateTime(row[5])
                };
            }

            return discount;    
        } 
     public bool AddDiscount(ClsDiscounts discount)
        {
            return false;
        }
     public bool EditDiscount(ClsDiscounts discount)
        {
            return false;
        }
     public bool DeleteDiscount(ClsDiscounts discount)
        {
            return false; 
        }


    }

}