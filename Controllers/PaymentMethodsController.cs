using ALAoun_Pos.Services.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ALAoun_Pos.Controllers
{
     [SessionCheckFilter]
    public class PaymentMethodsController : Controller
    {

        private readonly IPaymentMethodsService _paymentMethodsService;

        public PaymentMethodsController(IPaymentMethodsService paymentMethodsService)
        {
            _paymentMethodsService = paymentMethodsService;
        }
        public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");
           
            var paymentMethods = _paymentMethodsService.GetAllPaymentMethods(companyId.Value, branchId.Value);

            return View(paymentMethods);
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

            var paymentMethod = _paymentMethodsService.GetPaymentMethodById(companyId.Value, branchId.Value, id);

            return View(paymentMethod);
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
        public IActionResult GetIdAndNamePaymentMethods()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            var paymentMethods = _paymentMethodsService.GetAllPaymentMethods(companyId.Value, branchId.Value);
            
            var result = paymentMethods.Select(p => new
            {
                Id = p.PaymentId,
                Name = p.PaymentType 
            }).ToList(); 


            return Json(result);
        }
    }
}