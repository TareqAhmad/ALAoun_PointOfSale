using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services; 
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
    [SessionCheckFilter]
       public class CategoriesController : Controller
    {
             

         private readonly ICategoriesService _categoriesService;

         public  CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }  



        [HttpGet]
        public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            var Categories = _categoriesService.GetAllCategories(companyId.Value,branchId.Value); 

            return View(Categories); 
        }


        public IActionResult Create()
        {
            return View(); 
        }

       public IActionResult Edit(int id)
        {

           int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
           int? branchId = HttpContext.Session.GetInt32("BranchId"); 

            var Category = _categoriesService.GetCategoriesById(companyId.Value,branchId.Value,id); 

            return View(Category); 



            
        }


         public IActionResult Delete()
        {
            return View(); 
        }


        [HttpGet]
        public IActionResult GetAllCategories()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
           int? branchId = HttpContext.Session.GetInt32("BranchId"); 

  
            var Categories = _categoriesService.GetAllCategories(companyId.Value,branchId.Value); 

            return Json(Categories);
        }


        [HttpGet]
        public IActionResult GetIdAndNameCategories()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
           int? branchId = HttpContext.Session.GetInt32("BranchId"); 

            var Categories = _categoriesService.GetAllCategories(companyId.Value,branchId.Value); 
             
             var result = Categories.Select(c => new 
             {
                 Id = c.CategoryId,
                 Name = c.CategoryName
             }).ToList();


            return Json(result);
        }
   
   
        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryDto categoryDto)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 
           
             if(categoryDto == null || string.IsNullOrEmpty(categoryDto.categoryName))
            {
                return Json(new { success = false, message = "بيانات الفئة غير صالحة." });
            }
            
            categoryDto.companyId = companyId.Value;
            categoryDto.branchId = branchId.Value;

            var result = _categoriesService.AddCategory(categoryDto); 
            
            if(result)
            {
                return Json(new { success = true, message = "تم إضافة الفئة بنجاح." });
            }
            else
            {
                return Json(new { success = false, message = "فشل في إضافة الفئة." });
            }
    
        }
   
    }
    


}