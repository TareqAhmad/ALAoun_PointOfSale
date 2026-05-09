using Microsoft.AspNetCore.Mvc; 

using ALAoun_Pos.Services.interfaces;
using ALAoun_Pos.Models;
using System.Text.Json;

namespace ALAoun_Pos.Controllers
{
    [SessionCheckFilter]
    public class PendingInvoicesController : Controller
    {
        
        private readonly IPendingInvoicesService _pendingInvoicesService; 

        public PendingInvoicesController(IPendingInvoicesService pendingInvoicesService)
        {
            _pendingInvoicesService = pendingInvoicesService;
        }
       


       public IActionResult Index()
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
        public IActionResult GetAllPendingInvoices()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId") ; 
            int? branchId = HttpContext.Session.GetInt32("BranchId") ;
            int? posId = HttpContext.Session.GetInt32("PosId");

            var pendingInvoices = _pendingInvoicesService.GetAllPendingInvoices(companyId.Value,branchId.Value,posId.Value);

            return Json(pendingInvoices);
        }
       
  
       [HttpGet]
        public IActionResult GetPendingInvoiceById(int pendingInvoiceId)
        {
             int? companyId = HttpContext.Session.GetInt32("CompanyId") ; 
             int? branchId = HttpContext.Session.GetInt32("BranchId") ;
             int? posId = HttpContext.Session.GetInt32("PosId") ;  

            var pendingInvoice = _pendingInvoicesService.GetPendingInvoiceById(companyId.Value,branchId.Value,posId.Value,pendingInvoiceId);
    
            return Json(pendingInvoice);   
        }


        [HttpPost]
        public IActionResult AddPendingInvoice([FromBody] PendingInvoiceDto pendingInvoiceDto) 
        {   


            int? companyId = HttpContext.Session.GetInt32("CompanyId") ; 
            int? branchId = HttpContext.Session.GetInt32("BranchId") ; 
            int? posId = HttpContext.Session.GetInt32("PosId") ;
            int? userId = HttpContext.Session.GetInt32("UserId") ;


            var Result = _pendingInvoicesService.AddPendingInvoice(companyId.Value,branchId.Value,posId.Value,userId.Value,pendingInvoiceDto); 

            return Json(Result); 
        }

    }
}

