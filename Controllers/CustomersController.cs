using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
    
        public class CustomersController : Controller
    {
     
       private readonly ICustomersService _customersService; 

       public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService; 
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

            var Customers = _customersService.GetAllCustomers(companyId.Value,branchId.Value); 

            return View(Customers); 
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

            var Customer = _customersService.GetCustomerById(companyId.Value,branchId.Value,id); 

            return View(Customer); 


          
        }

        public IActionResult Delete()
        {
            return View(); 
        }

        [HttpGet]
       public IActionResult GetAllCustomers()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            if(companyId == null || branchId == null)
            {
                  return RedirectToAction("Home","Index"); 
            }

            var Customers = _customersService.GetAllCustomers(companyId.Value,branchId.Value); 

            return Json(Customers); 
        }


    }
}
