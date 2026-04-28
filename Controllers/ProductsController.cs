using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;


namespace ALAoun_Pos.Controllers
{
    public class ProductsController : Controller
    {
      
       private readonly IProductsService _productsService; 

       public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public IActionResult Index() {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 


            if (companyId == null || branchId == null)
            {
                return RedirectToAction("Home","index"); 
            }

             var products = _productsService.GetAllProducts(companyId.Value,branchId.Value);
    
            return View(products);
        }


        public IActionResult Create(){
            return View();
        }

         public IActionResult Edit(int id){
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 


            if (companyId == null || branchId == null)
            {
                return RedirectToAction("Home","index"); 
            }

             var product = _productsService.GetProductById(companyId.Value,branchId.Value,id);
    
            return View(product);
        }
      

         public IActionResult Delete(){
            return View();
        }

        [HttpGet]
        public IActionResult GetAllProducts(){
            
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 


            if (companyId == null || branchId == null)
            {
                return RedirectToAction("Home","index"); 
            }

             var products = _productsService.GetAllProducts(companyId.Value,branchId.Value);
    
            return Json(products);
        }
       
       [HttpGet]
       public IActionResult GetProduct(int id)
       {
          return View(); 
       }

      [HttpGet]
       public IActionResult GetTaxRateForProduct(int id)
       {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 

            if (companyId == null || branchId == null)
            {
                return RedirectToAction("Home","index"); 
            }

          return View(); 
       }

    }
}