using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using System.Net.NetworkInformation;
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
    [SessionCheckFilter]
    public class PurchasesController : Controller
    {
        
        private readonly IPurchasesService _purchasesService; 

        public PurchasesController(IPurchasesService purchasesService)
        {
            _purchasesService = purchasesService;
        }


        public IActionResult Index()
        {
             int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            
             var purchaseInvoices = _purchasesService.GetAllPurchaseInvoices(companyId.Value,branchId.Value); 

            return View(purchaseInvoices); 
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


        [HttpPost]
        public IActionResult AddInvoice([FromBody] InvoiceDto invoiceDto)
        {

            if(invoiceDto == null)
            {
                return Json(new {success= false,message="البيانات غير مكتملة"});
            }

            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId"); 
            int? posId = HttpContext.Session.GetInt32("PosId");
            int? userId = HttpContext.Session.GetInt32("UserId");  

            invoiceDto.InvoiceDate = DateTime.Now; 
            invoiceDto.CompanyId= companyId.Value; 
            invoiceDto.BranchId = branchId.Value;
            invoiceDto.PosId = posId.Value;
            invoiceDto.UserId = userId.Value; 


            var result = _purchasesService.AddPurchaseInvoice(invoiceDto); 
            

           return Json(new{success = false,message = "حدث خطا اثناء حفظ الفاتورة"});  
        }

    }
}