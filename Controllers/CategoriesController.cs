using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services; 
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
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

           if (companyId == null || branchId == null)
            {
                return RedirectToAction("Home","index"); 
            }

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

           if (companyId == null || branchId == null)
            {
                return RedirectToAction("Home","index"); 
            }

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

           if (companyId == null || branchId == null)
            {
                return RedirectToAction("Home","index"); 
            }
            
            var Categories = _categoriesService.GetAllCategories(companyId.Value,branchId.Value); 

            return Json(Categories);
        }
    }


}