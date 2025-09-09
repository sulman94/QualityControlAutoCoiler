using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ProjectX.Helper
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionExpiry : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = filterContext.HttpContext;
            string UserName = context.Session.GetString("UserName");
            string UserId = context.Session.GetString("UserId");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserId))
            {
                filterContext.Result = new RedirectToPageResult("/Account/Logout", new { area = "Identity" });
                return;
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
