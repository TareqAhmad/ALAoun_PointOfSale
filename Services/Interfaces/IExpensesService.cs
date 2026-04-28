using ALAoun_Pos.Models; 


namespace ALAoun_Pos.Services.interfaces
{
    
    public interface IExpensesService
    {
        
       public List<ClsExpenses> GetAllExpenses(int companyId,int branchId);

       public ClsExpenses GetExpenseById(int companyId,int branchId,int Id);

       public bool AddExpense(int companyId,int branchId,ClsExpenses expenses); 

       public bool EditExpense(int companyId,int branchId,ClsExpenses expenses); 

       public bool DeleteExpense(int companyId,int branchId,ClsExpenses expenses); 

    }



}