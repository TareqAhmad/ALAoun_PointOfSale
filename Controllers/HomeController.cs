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



        [HttpPost] 
        public ClsUserInfo Login([FromBody] ClsLogin model)
        { 
            ClsUserInfo login = new ClsUserInfo(); 


            login = _homeService.Login(model.CompanyId,model.BranchId,model.PosId,model.UserId,model.UserPassword);
            

            HttpContext.Session.SetInt32("CompanyId", login.CompanyId);
            HttpContext.Session.SetInt32("BranchId", login.BranchId);
            HttpContext.Session.SetInt32("PosId", login.PosId);
            HttpContext.Session.SetInt32("UserId", login.UserId);
            HttpContext.Session.SetInt32("RoleId",login.RoleId);
            HttpContext.Session.SetString("UserName", login.UserName);
            HttpContext.Session.SetString("CompanyName",login.CompanyName);
            HttpContext.Session.SetString("BranchName",login.BranchName);
            HttpContext.Session.SetString("PosName",login.PosName);

            return login;
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