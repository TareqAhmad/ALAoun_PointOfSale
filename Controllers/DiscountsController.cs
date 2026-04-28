using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;

using ALAoun_Pos.Services.interfaces;


namespace ALAoun_Pos.Controllers
{

    public class DiscountsController : Controller
    {


        private readonly IDiscountsService _discountsService;

        public DiscountsController(IDiscountsService discountsService)
            {
                _discountsService = discountsService;
            }

        public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");
           
            if (companyId == null || branchId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var discounts = _discountsService.GetAllDiscounts(companyId.Value, branchId.Value);
            return View(discounts);
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

            var discount = _discountsService.GetDiscountById(companyId.Value, branchId.Value, id);
           
            return View(discount);
       
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