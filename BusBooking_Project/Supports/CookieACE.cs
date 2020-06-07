using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supports
{
    public class CookieCuaSang
    {
        public static void Set(HttpContext httpContext, string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = expireTime.HasValue ? DateTime.Now.AddMinutes(expireTime.Value) : DateTime.Now.AddMilliseconds(10);
            httpContext.Response.Cookies.Append(key, value, option);
        }

        public static void Remove(HttpContext httpContext, string key)
        {
            httpContext.Response.Cookies.Delete(key);
        }
    }
}
