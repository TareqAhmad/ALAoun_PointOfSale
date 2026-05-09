using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;


namespace ALAoun_Pos.Controllers
{
    [SessionCheckFilter]
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

             var products = _productsService.GetAllProducts(companyId.Value,branchId.Value);
    
            return View(products);
        }

        public IActionResult Create(){
            return View();
        }

         public IActionResult Edit(int id)
         {
            
             int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 



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

             var products = _productsService.GetAllProducts(companyId.Value,branchId.Value);
    
            return Json(products);
        }

        [HttpGet]
        public IActionResult GetIdAndNameProducts(){
            
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 


    
             var products = _productsService.GetAllProducts(companyId.Value,branchId.Value);
                var result = products.Select(p => new { 
                    Id = p.ProductId,
                   Name = p.ProductName,
                   icon = p.iconId }).ToList();  

            return Json(result);
        }
       
       [HttpGet]
       public IActionResult GetProduct(int id)
       {
          return View(); 
       }

       [HttpGet]
       public IActionResult GetProductsForOperation()
       {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 

            var products = _productsService.GetInfoProductsForOperation(companyId.Value,branchId.Value); 

            return Json(products); 
       }
       


       [HttpPost]
       public IActionResult AddProduct([FromBody] ProductDto product)
       {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 
             
            if (product == null)
            {
                return Json(new { success = false, message = "بيانات المنتج غير صحيحة." });
            }

            product.companyId = companyId.Value;
            product.branchId = branchId.Value;

           var result = _productsService.AddProduct(product);
           
           if(result)
            {
                return Json(new { success = true, message = "تم إضافة المنتج بنجاح." });
            }
            else
            {
                return Json(new { success = false, message = "فشل في إضافة المنتج." });
            }

        
       }
    }
}