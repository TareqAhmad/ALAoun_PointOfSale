using Microsoft.AspNetCore.Mvc; 
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;


namespace ALAoun_Pos.Controllers
{
    public class ExpensesController : Controller
    {
        
        private readonly IExpensesService _expensesService; 

        public ExpensesController(IExpensesService expensesService)
        {
            _expensesService = expensesService; 
        }

        public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 

            if (companyId == null || branchId == null)
            {
                return RedirectToAction("Home","Index"); 
            }

            var Expenses = _expensesService.GetAllExpenses(companyId.Value,branchId.Value); 
            
            return View(Expenses); 
        }

       public IActionResult Details()
        {
            return View(); 
        }
        public IActionResult Create()
        {
            return View(); 
        }

       public IActionResult Edit(int id)
        {
             int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 

            if (companyId == null || branchId == null)
            {
                return RedirectToAction("Home","Index"); 
            }

            var Expense = _expensesService.GetExpenseById(companyId.Value,branchId.Value,id); 
            
            return View(Expense); 
        }
        
       public IActionResult Delete()
        {
            return View(); 
        } 

    }
}