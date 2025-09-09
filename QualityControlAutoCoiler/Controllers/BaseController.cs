using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ProjectX.Controllers
{
    public class BaseController : Controller
    {
        public int GetUserId
        {
            get
            {
                int userId = 0;
                string loggedInUser = (HttpContext.Session == null ? String.Empty : HttpContext.Session.GetString("UserId"));

                if (!String.IsNullOrWhiteSpace(loggedInUser))
                    userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

                return userId;
            }
        }
    }
}
