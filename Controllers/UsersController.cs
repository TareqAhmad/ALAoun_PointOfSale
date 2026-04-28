using Microsoft.AspNetCore.Mvc; 
using ALAoun_Pos.Models; 
using ALAoun_Pos.Services.interfaces;

namespace ALAoun_Pos.Controllers
{
    public class UsersController : Controller
    {

       private readonly IUsersServices _usersServices;

       public UsersController(IUsersServices usersServices)
        {
             _usersServices = usersServices;
        }

        private IActionResult ExitApplication()
        {
             return RedirectToAction("index","Home"); 
        }


        
        [HttpGet]
        public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");             

            if(companyId == null || branchId == null)
            {
              return  ExitApplication();  
            }
            
            var users = _usersServices.GetAllUsers(companyId.Value,branchId.Value);

            return View(users);
        }

        [HttpPost]
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
        public List<ClsUsers> GetAllUsers(int companyId,int branchId)
        {
            List<ClsUsers> users = new List<ClsUsers>(); 
          
            users = _usersServices.GetAllUsers(companyId,branchId); 

            return users; 
            
        }
        

       [HttpGet]
       public IActionResult GetUserPrivileges(int userId)
       {
          int? companyId = HttpContext.Session.GetInt32("CompanyId");
          int? branchId = HttpContext.Session.GetInt32("BranchId");

          if(companyId == null || branchId == null)
          {
              return ExitApplication();
          }

           var userPrivileges = _usersServices.GetUserRolePrivilegesById(companyId.Value, branchId.Value, userId);
              
            return  Json(userPrivileges);
              

       }



    }
}