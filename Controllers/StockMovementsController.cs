using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
      [SessionCheckFilter]
    public class StockMovementsController : Controller
    {
        private readonly IStockMovementsService _stockMovementsService; 

        public StockMovementsController(IStockMovementsService stockMovementsService)
        {
            _stockMovementsService = stockMovementsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");


             var stockMovements = _stockMovementsService.GetAllStockMovements(companyId.Value,branchId.Value);

            return View(stockMovements);

        }
        public IActionResult Create()
        {
            return View();  
        }

        public IActionResult Edit(int id)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

             var stockMovements = _stockMovementsService.GetStockMovementsById(companyId.Value,branchId.Value,id);

            return View(stockMovements);

        }

        public IActionResult Delete(int id)
        {
            return View();
        }

    }
    
    
    }
