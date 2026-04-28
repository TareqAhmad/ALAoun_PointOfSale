
using Microsoft.Data.SqlClient;
using System.Data;
using ALAoun_Pos.Models;
using ALAoun_Pos.Data;
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Services
{
    
    public class ExpensesService : IExpensesService
    {


      private readonly DbHelper _dbHelper; 

      public   ExpensesService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper; 
        }
      public List<ClsExpenses> GetAllExpenses(int companyId,int branchId)
        {
           List<ClsExpenses> expenses = new List<ClsExpenses>(); 

           string query = @"SELECT expenseId,expenseDate,description,amount
                           FROM Expenses
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
                var expense = new ClsExpenses
                {
                    expenseId = Convert.ToInt32(row[0]),
                    expenseDate = Convert.ToDateTime(row[1]),
                    description = row[2].ToString(),
                    amount = Convert.ToDecimal(row[3])
                }; 

                expenses.Add(expense); 
            }

            return expenses; 
        }

       public ClsExpenses GetExpenseById(int companyId,int branchId,int Id)
        {
            ClsExpenses expense = null;

            string query = @"SELECT expenseId,expenseDate,description,amount
                           FROM Expenses
                           WHERE companyId =@companyId
                           AND branchId = @branchId
                           AND expenseId = @expenseId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@companyId", companyId),
                new SqlParameter("@branchId", branchId),        
                new SqlParameter("@expenseId", Id)
            };

            DataTable dt = _dbHelper.Select(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                expense = new ClsExpenses
                {
                    expenseId = Convert.ToInt32(row[0]),
                    expenseDate = Convert.ToDateTime(row[1]),
                    description = row[2].ToString(),
                    amount = Convert.ToDecimal(row[3])
                };
            }
            return expense;               
        }

       public bool AddExpense(int companyId,int branchId,ClsExpenses expenses)
        {
            return false; 
        }

       public bool EditExpense(int companyId,int branchId,ClsExpenses expenses)
        {
            return false; 
        } 

       public bool DeleteExpense(int companyId,int branchId,ClsExpenses expenses)
        {
            return false; 
        }
    }
}