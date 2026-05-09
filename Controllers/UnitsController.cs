using Microsoft.AspNetCore.Mvc;

using ALAoun_Pos.Models;
using ALAoun_Pos.Services;
using ALAoun_Pos.Services.interfaces;

namespace AL_Aoun_Pos.controllers
{
    [SessionCheckFilter]
    public class UnitsController : Controller
    {
     
       private readonly IUnitsService _unitsService; 

       public UnitsController(IUnitsService unitsService)
        {
            _unitsService = unitsService; 
        }

        [HttpGet]
        public IActionResult Index()
        {   
            
            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            var units = _unitsService.GetAllUnits(companyId.Value, branchId.Value );
           
            return View(units);

           
        }
        
        [HttpGet]
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


        public IActionResult Details()
        {
            return View();
        }


         public IActionResult GetIdAndNameUnits()
         {
             int? companyId = HttpContext.Session.GetInt32("CompanyId");
             int? branchId = HttpContext.Session.GetInt32("BranchId");
             
             var units = _unitsService.GetAllUnits(companyId.Value, branchId.Value);
            
             var result = units.Select(u => new {
                Id = u.UnitId,
               Name = u.UnitName }).ToList();

             return Json(result);
         }

        [HttpPost]
        public IActionResult AddUnit([FromBody] UnitsDto unit)
        {
           
            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");
           
            if(unit == null || string.IsNullOrEmpty(unit.unitName))
            {
                  return Json(new { success = false, message = "اسم الوحدة مطلوب" });
            }

            var result = _unitsService.AddUnit(unit.unitName, companyId.Value, branchId.Value);
            
            if(result)
            {
                return Json(new{ success = true, message = "تمت إضافة الوحدة بنجاح" }); 
            }

             return Json(new{ success = false, message = "فشل في إضافة الوحدة" });
         }


    }


}