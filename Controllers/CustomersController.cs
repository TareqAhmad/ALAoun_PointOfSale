using Microsoft.AspNetCore.Mvc;
using ALAoun_Pos.Services.interfaces;
using ALAoun_POS.Models;

namespace ALAoun_Pos.Controllers
{
    [SessionCheckFilter]
        public class CustomersController : Controller
    {
     
       private readonly ICustomersService _customersService; 

       public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService; 
        }


        [HttpGet]
         public IActionResult Index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            var Customers = _customersService.GetAllCustomers(companyId.Value,branchId.Value); 

            return View(Customers); 
        }

         public IActionResult Create()
        {
            return View(); 
        }

        public IActionResult Edit(int id)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            var Customer = _customersService.GetCustomerById(companyId.Value,branchId.Value,id); 

            return View(Customer); 


          
        }

        public IActionResult Delete(int id)
        {
            return View(); 
        }

        [HttpGet]
       public IActionResult GetAllCustomers()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            var Customers = _customersService.GetAllCustomers(companyId.Value,branchId.Value); 

            return Json(Customers); 
        }


       [HttpGet]
       public IActionResult GetIdAndNameCustomers()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            var Customers = _customersService.GetAllCustomers(companyId.Value,branchId.Value); 
            
            var result = Customers.Select(c => new { 
                Id = c.CustomerId,
                Name = c.CustomerName
            }).ToList();
            
            return Json(result); 
        }
       

       [HttpPost]
       public IActionResult AddCustomer([FromBody] CustomerDto customerDto)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");
           

           if (customerDto == null)
            {
                return Json(new { success = false, message = "Invalid customer data." });   
            }

            customerDto.companyId = companyId.Value;
            customerDto.branchId = branchId.Value;

            var result = _customersService.AddCustomer(customerDto); 

            if (result)
            {
                return Json(new { success = true, message = "Customer added successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Failed to add customer." });
            }

        }

         [HttpPut]
        public IActionResult UpdateCustomer( [FromBody] CustomerDto customerDto)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            var updatedCustomer = _customersService.EditCustomer(customerDto); 

            if (updatedCustomer == null)
            {
                return NotFound(); 
            }

            return Json(updatedCustomer); 
        }

         [HttpDelete]
        public IActionResult DeleteCustomer([FromBody] CustomerDto customerDto)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");

            bool isDeleted = _customersService.DeleteCustomer(customerDto); 

            if (!isDeleted)
            {
                return NotFound(); 
            }

            return NoContent(); 
        }
    }
}
