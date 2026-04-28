using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
    public class BranchesController : Controller
    {
      
       private readonly IBranchesService _branchesService; 

     
       public BranchesController(IBranchesService branchesService){

             _branchesService = branchesService;
       }
      

        public IActionResult Index()
        {
            
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 

            if(companyId == null)
            {
                 return RedirectToAction("Home","index"); 
            }

            var branches = _branchesService.GetAllBranches(companyId.Value);
    
            return View(branches);
        }

         public List<ClsBranches> GetAllBranches(int companyId) 
        {  
            List<ClsBranches> BranchesList = new List<ClsBranches>(); 

            BranchesList = _branchesService.GetAllBranches(companyId); 
            
            return BranchesList;
        }


         [HttpGet]  
         public ClsBranches GetBranchById(int id)
        {
            var branch = new ClsBranches(); 
             
            return branch; 
        }
         

       

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

         public IActionResult Edit()
        {
            return View();
        }

    
         public IActionResult Delete()
        {
            return View();
        }

    }
    
    
    
    
}
