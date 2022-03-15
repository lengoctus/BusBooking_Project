using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.SupportsTu
{
    public class CookieSupport
    {
        public readonly static string InfoBooking = "InfoBooking";

        public static void Set(HttpContext context, string key, string value, int? expireTime)
        {
            CookieOptions options = new CookieOptions();
            if (expireTime.HasValue)
            {
                options.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            }
            else
            {
                options.Expires = DateTime.Now.AddMilliseconds(10);
            }

            context.Response.Cookies.Append(key, value, options);
        }


        public static void Remove(HttpContext context, string key)
        {
            context.Response.Cookies.Delete(key);
        }

        public static bool CheckCookieExists(HttpContext httpContext, string key) {
            if (httpContext.Request.Cookies[key] != null)
            {
                return true;
            }
            return false;
        }
    }
}
