using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using BusBooking_Project.Models.ModelsView;

namespace Supports
{
    public class SercurityManagerCuaSang
    {
        public static void Login(HttpContext httpContext, AccountView user, string cheme)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(GetUserClaim(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            httpContext.SignInAsync(cheme, claimsPrincipal);
        }

        public static void Logout(HttpContext httpContext, string cheme)
        {
            httpContext.SignOutAsync(cheme);
        }

        private static IEnumerable<Claim> GetUserClaim(AccountView user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("id", user.Id + ""));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Role, user.Role + ""));
            return claims;
        }
    }
}
