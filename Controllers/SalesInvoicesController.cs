using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;


namespace ALAoun_Pos.Controllers
{
    
    public class SalesInvoicesController : Controller
    {
        
        
       private readonly ISalesInvoicesService _salesInvoicesService;

       public SalesInvoicesController(ISalesInvoicesService salesInvoicesService)
        {
             _salesInvoicesService = salesInvoicesService;
        }

        [HttpGet]
        public IActionResult Index()
        {

            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            int? posId = HttpContext.Session.GetInt32("PosId");

             if(companyId == null || branchId == null || posId == null)
            {
                  return RedirectToAction("Index","Home"); 
            }
         
             var salesInvoices = _salesInvoicesService.GetAllSalesInvoices(companyId.Value,branchId.Value,posId.Value);  

             return View(salesInvoices);

     
        }
          
         [HttpPost] 
        public IActionResult Create([FromBody] SalesInvoiceDto salesInvoiceDto)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            int? posId = HttpContext.Session.GetInt32("PosId"); 
            int? userId = HttpContext.Session.GetInt32("UserId");

            if(companyId == null || branchId == null || posId == null || userId == null)
            {
                  return RedirectToAction("Index","Home"); 
            }

            var Result =  _salesInvoicesService.AddSalesInvoice(companyId.Value,branchId.Value,posId.Value,userId.Value,salesInvoiceDto); 

             return Json(Result);
        }

         public IActionResult Details()
        {

            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            int? posId = HttpContext.Session.GetInt32("PosId"); 

            if(companyId == null || branchId == null || posId == null)
            {
                  return RedirectToAction("Index","Home"); 
            }

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
        public IActionResult GetAllSalesInvoices()
        {

            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            int? posId = HttpContext.Session.GetInt32("PosId");

             if(companyId == null || branchId == null || posId == null)
            {
                  return RedirectToAction("Index","Home"); 
            }
         
             var salesInvoices = _salesInvoicesService.GetAllSalesInvoices(companyId.Value,branchId.Value,posId.Value);  

             return Json(salesInvoices);

     
        }

       [HttpGet]
        public IActionResult GetSaleInvoice(int id)
        {

            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            int? posId = HttpContext.Session.GetInt32("PosId");

             if(companyId == null || branchId == null || posId == null)
            {
                  return RedirectToAction("Index","Home"); 
            }
         
             var salesInvoices = _salesInvoicesService.GetAllSalesInvoices(companyId.Value,branchId.Value,posId.Value);  

             return Json(salesInvoices);

     
        }

    }

}