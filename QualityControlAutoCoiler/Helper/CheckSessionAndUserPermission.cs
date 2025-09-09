using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
namespace ProjectX.Helper
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionAndUserPermission : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool IsAllow = false;
            HttpContext context = filterContext.HttpContext;
            string UserName = context.Session.GetString("UserName");
            string UserId = context.Session.GetString("UserId");

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserId))
            {
                filterContext.Result = new RedirectToPageResult("/Account/Logout", new { area = "Identity" });
                return;
            }
            var routeData = context.GetRouteData().Values;
            string currentController = routeData["controller"]?.ToString();
            string currentAction = routeData["action"]?.ToString();
            string UserPermissions = context.Session.GetString("UserPermissions");

            if (!string.IsNullOrEmpty(UserPermissions))
            {
                var serializedpermissions = JsonSerializer.Deserialize<List<UserPermissionsModel>>(UserPermissions);
                bool IsAjax = IsAjaxRequest.IsAjaxRequestt(context.Request);
                IsAllow = serializedpermissions.Any(x => x.ControllerName == currentController && x.ActionMethodName == currentAction);
                if (!IsAllow && !IsAjax)
                {
                    filterContext.Result = new RedirectResult("~/Home/Unauthorize");
                    return;
                }
                if (!IsAllow && IsAjax)
                {
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }
            }


            base.OnActionExecuting(filterContext);
        }

    }

}
