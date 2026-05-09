using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{


    public class HomeController : Controller
    {
     
       private readonly IHomeService _homeService; 

       public HomeController(IHomeService homeService)
        {
            _homeService = homeService; 
        }


         public IActionResult Index()
        {
            return View(); 
        }


        [HttpPost] // تغيير إلى Post للأمان
        public IActionResult Login([FromBody] ClsLogin model)
        { 
             if (model == null) return BadRequest();

            var user = _homeService.Login(model.CompanyId, model.BranchId, model.PosId, model.UserId, model.UserPassword);
            
            // 1. التحقق من وجود المستخدم
            if (user == null) return Json(new { success = false, message = "فشل تسجيل الدخول. يرجى التحقق من بيانات الاعتماد." });
            // 2. التحقق من الصلاحيات
            if (user.RoleId == 0)return Json(new { success = false, message = "ليس لديك صلاحية الوصول إلى النظام." });
            // 3. التحقق من حالة الحساب
            if (!user.IsActive) return Json(new { success = false, message = "الحساب بحاجة لتجديد الاشتراك. يرجى الاتصال بالمسؤول." });

             // 4. إعداد الجلسة
             HttpContext.Session.SetInt32("UserId", user.UserId);
             HttpContext.Session.SetInt32("loginTypeId", 1); // تسجيل دخول اول مرة
             HttpContext.Session.SetInt32("RoleId", user.RoleId);
             HttpContext.Session.SetString("UserName", user.UserName);
             HttpContext.Session.SetInt32("CompanyId", user.CompanyId);
             HttpContext.Session.SetInt32("BranchId", user.BranchId);
             HttpContext.Session.SetInt32("PosId", user.PosId);
             HttpContext.Session.SetString("CompanyName", user.CompanyName);
             HttpContext.Session.SetString("BranchName", user.BranchName);
             HttpContext.Session.SetString("PosName", user.PosName);
   
            return Json(new { data = user, success = true, message = "تم تسجيل الدخول بنجاح." });
        }


       
         public IActionResult Dashboard()
        {
            return View(); 
        }


        [HttpGet] 
        public IActionResult Cashier()
        {
             var userId = HttpContext.Session.GetInt32("CompanyId");
             var userName =  HttpContext.Session.GetString("UserName"); 
             var companyName = HttpContext.Session.GetString("CompanyName"); 
             var branchName =  HttpContext.Session.GetString("BranchName");
             var posName =  HttpContext.Session.GetString("PosName");
              
              ViewBag.userId = userId;
              ViewBag.userName = userName; 
              ViewBag.companyName = companyName; 
              ViewBag.branchName = branchName; 
              ViewBag.posName = posName; 
 
            return View(); 
        }

        public IActionResult Logout()
        {
            return View();
       }
      
 
    }
}