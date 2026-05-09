using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services; 
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
    public class CompaniesController(ICompaniesService companiesService) : Controller
    {



        // Admin System
         public List<ClsCompanies> GetCompanies()
        {
            var companies = new List<ClsCompanies>(); 

            return companies; 
        }


        [HttpGet]  
         public ClsCompanies GetCompanyById(int id)
        {
            var company = new ClsCompanies(); 

            return company; 
        }

        [HttpGet]
        public ClsCompanies GetCompanyByUserCompany(string userCompany)
        {
             var Company = new ClsCompanies(); 
                
             Company = companiesService.GetCompanyByUserCompany(userCompany); 
             
             if (Company != null)
                 return Company; 

             return null;     
   
        }

        public IActionResult Index()
        {
            var companies = new List<ClsCompanies>();
    
            return View(companies);
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

        [HttpGet]
        public IActionResult GetIdAndNameCompanies(string userCompany)
        {
           
            var Company = companiesService.GetCompanyByUserCompany(userCompany); 
             
             if (Company == null) return NotFound();

             var result =  new {
                
                Id = Company.CompanyId,
                Name = Company.CompanyName
              };

             return Json(result);     
   
        }
        
        
        


    }
}
