using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
    
      [SessionCheckFilter]
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


            var supplier = _suppliersService.GetSupplierById(companyId.Value,branchId.Value,id); 

            return View(supplier); 
        }

        public IActionResult Delete()
        {
            return View(); 
        }
        
        public IActionResult Details()
        {
            return View();  
        }

        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            var suppliers = _suppliersService.GetAllSuppliers(companyId.Value,branchId.Value); 

            return View(suppliers);
        }

        [HttpGet]
        public IActionResult GetIdAndNameSuppliers()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");


            var suppliers = _suppliersService.GetAllSuppliers(companyId.Value,branchId.Value); 
            
            var result = suppliers.Select(s => new { 
                
                Id =  s.SupplierId,
                Name = s.SupplierName }).ToList(); 
            
            return Json(result);
        }
        
        [HttpPost]
        public IActionResult AddSupplier([FromBody] SupplierDto supplierDto)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            if (supplierDto == null)
            {
                return Json(new { success = false, message = "Invalid supplier data." });
            } 

            supplierDto.companyId = companyId.Value;
            supplierDto.branchId = branchId.Value;

            var result = _suppliersService.AddSupplier(supplierDto); 
            
            if(result)
            {
                return Json(new { success = true, message = "Supplier added successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Failed to add supplier." });
            }

     
        }
    
    }
}