using Microsoft.AspNetCore.Mvc.Filters;

namespace Office.Web.Attributes
{
    public class RequireLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserId") == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectToActionResult("Index", "Login", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
