using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using System.Net.NetworkInformation;
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
    public class PurchaseInvoicesController : Controller
    {
        
        private readonly IPurchaseInvoicesService _purchaseInvoiceService; 

        public PurchaseInvoicesController(IPurchaseInvoicesService purchaseInvoicesService)
        {
            _purchaseInvoiceService = purchaseInvoicesService;
        }


        public IActionResult Index()
        {
             int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            if(companyId == null || branchId == null)
            {
                  return RedirectToAction("Home","Index"); 
            }
             var purchaseInvoices = _purchaseInvoiceService.GetAllPurchaseInvoices(companyId.Value,branchId.Value); 

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

    }
}