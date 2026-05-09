

using System.Diagnostics;
using ALAoun_Pos.Models;
using ALAoun_Pos.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ALAoun_Pos.Controllers
{
    public class AttendanceLogsController : Controller
    {

        private readonly IAttendanceLogsService _attendanceLogsService; 
        public AttendanceLogsController(IAttendanceLogsService attendanceLogsService)
        {
            _attendanceLogsService = attendanceLogsService;
        }

        [HttpPost]
        public IActionResult SetAuditLogin([FromBody] AuditLoginDto auditLoginDto)
        {
            if(auditLoginDto == null || auditLoginDto.userId == null  || string.IsNullOrEmpty(auditLoginDto.userName) ) {
                return Json(new  {success = false,loginType = -1,message = "البيانات غير مكتملة"}); 
            }

            int? companyId = HttpContext.Session.GetInt32("CompanyId");
            int? branchId = HttpContext.Session.GetInt32("BranchId");
            int? posId = HttpContext.Session.GetInt32("PosId");
            int  nextLogTypeId = 0; 
            

            if (!companyId.HasValue || !branchId.HasValue) {
                return Json(new { success = false, message = "انتهت الجلسة، يرجى إعادة تسجيل الدخول" });
            }
            
           
            if (auditLoginDto.logTypeId == null || auditLoginDto.logTypeId == 0 )
            {
                
        
               var resultAllAttendance = _attendanceLogsService.GetAllAttendance(companyId.Value,branchId.Value,posId.Value); 

               var lastAttendance = resultAllAttendance
                              .Where(a => a.userId == auditLoginDto.userId)
                              .OrderByDescending(a=> a.logTime)
                              .FirstOrDefault(); 

                nextLogTypeId =(lastAttendance == null || lastAttendance.logTypeId == 2) ? 1 : 4;

            }
            
            auditLoginDto.logTypeId = (nextLogTypeId == 0) ?  auditLoginDto.logTypeId : nextLogTypeId; 
            auditLoginDto.logTime = DateTime.Now; 
            auditLoginDto.shiftDate = DateTime.Now; 
            auditLoginDto.notes =  (nextLogTypeId == 0) ? auditLoginDto.notes :  (nextLogTypeId ==  1) ? "تسجيل حركة بداية دوام" : "تسجيل حركة عودة من مغادرة";
            auditLoginDto.companyId = companyId.Value;
            auditLoginDto.branchId = branchId.Value; 
            auditLoginDto.posId = posId.Value;  
             
            var result = _attendanceLogsService.AddAttendance(auditLoginDto); 
            if (result)
            {
                string msg = auditLoginDto.logTypeId switch
                {
                    1 => "تم تسجيل بداية الدوام بنجاح",
                    2 => "تم تسجيل نهاية الدوام بنجاح",
                    3 => "تم تسجيل مغادرة بنجاح",
                    4 => "تم تسجيل عودة من مغادرة بنجاح",
                    _ => "تمت العملية بنجاح"
                };
                
                return Json(new { success = true, loginType = auditLoginDto.logTypeId, message = msg });
             }

               return Json(new { success = false, message = "خطأ في تسجيل الدوام بقاعدة البيانات" });
          
        }
    }
}