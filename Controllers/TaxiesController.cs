using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;

using ALAoun_Pos.Services.interfaces;


namespace ALAoun_Pos.Controllers
{

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
           
            if (companyId == null || branchId == null)
            {
                return RedirectToAction("Index", "Home");
            }

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
           
            if (companyId == null || branchId == null)
            {
                return RedirectToAction("Index", "Home");
            }

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
    }
}