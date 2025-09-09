using Microsoft.AspNetCore.Http;
using System;

namespace ProjectX.Helper
{
    public class IsAjaxRequest
    {
        public static bool IsAjaxRequestt(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
    }
}
