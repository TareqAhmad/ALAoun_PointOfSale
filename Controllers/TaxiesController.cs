using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;

using ALAoun_Pos.Services.interfaces;


namespace ALAoun_Pos.Controllers
{
    [SessionCheckFilter]
    public class TaxiesController : Controller
    {


        private readonly ITaxiesService _taxiesService;

        public TaxiesController(ITaxiesService taxiesService)
            {
                _taxiesService = taxiesService;
            }

        public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");
           

            var taxies = _taxiesService.GetAllTaxies(companyId.Value);
            return View(taxies);
        }


        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
             int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");
           

            var tax = _taxiesService.GetTaxById(companyId.Value, id);
            return View(tax);
        }

        public IActionResult Details(int id)
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllTaxies()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");
           

            var taxies = _taxiesService.GetAllTaxies(companyId.Value);
           
            return View(taxies);
        }

                [HttpGet]
        public IActionResult GetIdAndNameTaxies()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");
           
            var taxies = _taxiesService.GetAllTaxies(companyId.Value);
              
            var result = taxies.Select(t => new { 
                
                Id =  t.taxId,
                Name = t.taxRate }).ToList(); 
            
            return Json(result);
        }
    }
}