using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;


namespace ALAoun_Pos.Controllers
{
    
    [SessionCheckFilter]
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

         
             var salesInvoices = _salesInvoicesService.GetAllSalesInvoices(companyId.Value,branchId.Value,posId.Value);  

             return View(salesInvoices);

     
        }
          
         [HttpPost] 
        public IActionResult Create([FromBody] InvoiceDto salesInvoiceDto)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            int? posId = HttpContext.Session.GetInt32("PosId"); 
            int? userId = HttpContext.Session.GetInt32("UserId");


            salesInvoiceDto.InvoiceDate  = DateTime.Now; 
            salesInvoiceDto.CompanyId = companyId.Value; 
            salesInvoiceDto.BranchId = branchId.Value; 
            salesInvoiceDto.PosId = posId.Value; 
            salesInvoiceDto.UserId = userId.Value; 


            var Result =  _salesInvoicesService.AddSaleInvoice(salesInvoiceDto); 

             return Json(Result);
        }

         public IActionResult Details()
        {

            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            int? posId = HttpContext.Session.GetInt32("PosId"); 

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
         
             var salesInvoices = _salesInvoicesService.GetAllSalesInvoices(companyId.Value,branchId.Value,posId.Value);  

             return Json(salesInvoices);

     
        }

       [HttpGet]
        public IActionResult GetSaleInvoice(int id)
        {

            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            int? posId = HttpContext.Session.GetInt32("PosId");

             if(posId == null)
            {
                  return RedirectToAction("Index","Home"); 
            }
         
             var salesInvoices = _salesInvoicesService.GetAllSalesInvoices(companyId.Value,branchId.Value,posId.Value);  

             return Json(salesInvoices);

     
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


            var result = _salesInvoicesService.AddSaleInvoice(invoiceDto); 
            

           return Json(new{success = false,message = "حدث خطا اثناء حفظ الفاتورة"});  
        }
   
   
   
    }

}