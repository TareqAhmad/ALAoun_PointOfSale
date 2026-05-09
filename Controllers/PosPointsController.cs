using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;
using Microsoft.AspNetCore.Mvc; 

namespace ALAoun_Pos.Controllers
{
 
    public class PosPointsController : Controller
    {
        
        private readonly IPosPointsService _posPointsService; 

        public PosPointsController(IPosPointsService posPointsService){
          
             _posPointsService = posPointsService;
          
        }

        [HttpGet]
         [SessionCheckFilter]
        public IActionResult index()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId"); 
            int? branchId = HttpContext.Session.GetInt32("BranchId");             

            var posPoints =  _posPointsService.GetAllPosPoints(companyId.Value,branchId.Value); 

            return View(posPoints); 
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

        public IActionResult Details()
        {
            return View(); 
        }


        [SessionCheckFilter]
        [HttpGet]
        public List<ClsPosPoints> GetAllPosPoints(int companyId, int branchId)
        { 

            var posPoints =  _posPointsService.GetAllPosPoints(companyId,branchId); 


            return posPoints; 
        }

        [HttpGet]
        public IActionResult GetIdAndNamePosPoints(int companyId, int branchId)
        { 

            var posPoints =  _posPointsService.GetAllPosPoints(companyId,branchId); 

             var result = posPoints.Select(p => new
             {
                    id = p.PosId,
                    name = p.PosName
             }).ToList();

            return Json(result); 
        }


    }

}