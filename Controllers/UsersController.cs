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

  

        [SessionCheckFilter]
        [HttpGet]
        public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");             
            
            var users = _usersServices.GetAllUsers(companyId.Value,branchId.Value,0);

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
          
          
           var users = _usersServices.GetAllUsers(companyId,branchId,0); 

            return users; 
            
        }


        [HttpGet]
        public IActionResult GetIdAndNameUsers(int companyId,int branchId,int posId = 0)
        {
         
           var users = _usersServices.GetAllUsers(companyId,branchId,posId); 
           
         
           var result = users.Select(u=> new {
                Id = u.UserId,
                Name = u.UserName
                
            }).ToList();

            return Json(result);
            
        }

       [HttpGet]
       public IActionResult GetUserPrivileges(int userId)
       {
          int? companyId = HttpContext.Session.GetInt32("CompanyId");
          int? branchId = HttpContext.Session.GetInt32("BranchId");


           var userPrivileges = _usersServices.GetUserRolePrivilegesById(companyId.Value, branchId.Value, userId);
              
            return  Json(userPrivileges);
              

       }



    }
}