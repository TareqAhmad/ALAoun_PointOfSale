using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
    
        public class SuppliersController : Controller
    {
     
       private readonly ISuppliersService _suppliersService; 

       public SuppliersController(ISuppliersService suppliersService)
        {
            _suppliersService = suppliersService; 
        }


        [HttpGet]
         public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            if(companyId == null || branchId == null)
            {
                  return RedirectToAction("Home","Index"); 
            }

            var suppliers = _suppliersService.GetAllSuppliers(companyId.Value,branchId.Value); 

            return View(suppliers); 
        }

         public IActionResult Create()
        {
            return View(); 
        }

        public IActionResult Edit(int id)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            if(companyId == null || branchId == null)
            {
                  return RedirectToAction("Home","Index"); 
            }

            var supplier = _suppliersService.GetSupplierById(companyId.Value,branchId.Value,id); 

            return View(supplier); 
        }

        public IActionResult Delete()
        {
            return View(); 
        }
        

    }
    
    
    }