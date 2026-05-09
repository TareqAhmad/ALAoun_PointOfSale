

using ALAoun_POS.Models;
using ALAoun_POS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ALAoun_POS.Controllers
{
    [SessionCheckFilter]
    public class SettingsAppController : Controller
    {


        private readonly ISettingsAppService _settingsAppService;


        public SettingsAppController(ISettingsAppService settingsAppService)
        {
            _settingsAppService = settingsAppService;
        }
        
        public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            var settingsAppList = _settingsAppService.GetAllSettingsApp(companyId.Value, branchId.Value);

            return View(settingsAppList);
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