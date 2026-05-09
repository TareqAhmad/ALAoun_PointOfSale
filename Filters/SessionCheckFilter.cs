using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class SessionCheckFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // 1. الوصول إلى الجلسة من خلال السياق (Context)
        var session = context.HttpContext.Session;
        int? companyId = session.GetInt32("CompanyId");
        int? branchId = session.GetInt32("BranchId");

        // 2. التحقق: إذا كانت القيم فارغة
        if (companyId == null || branchId == null)
        {
            // 3. توجيه المستخدم لصفحة تسجيل الدخول أو الرئيسية
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }

        base.OnActionExecuting(context);
    }
}