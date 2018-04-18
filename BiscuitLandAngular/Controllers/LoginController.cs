using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BiscuitLandAngular.Models;

namespace BiscuitLandAngular.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        [Route("api/login")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(Login login)
        {
            DBHelper dBHelper = HttpContext.RequestServices.GetService(typeof(DBHelper)) as DBHelper;

            Login loginuser = new Login(dBHelper);
            bool isvalidlogin = loginuser.ValidateLogin(login.UserName, login.Password);

            if (isvalidlogin)
            {
            //    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, login.UserName, DateTime.Now, DateTime.Now.AddMinutes(30), true, "");
            //    String cookiecontents = FormsAuthentication.Encrypt(authTicket);
            //    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookiecontents) { Expires = authTicket.Expiration, Path = FormsAuthentication.FormsCookiePath };
            //    HttpContext.Current.Response.Cookies.Add(cookie);

                return Ok();
            }
            else
            {
                //LogoutTasks();
                return new NotFoundObjectResult("Authentication Exception");
            }
        }
    }
}