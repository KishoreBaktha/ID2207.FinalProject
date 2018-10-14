using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Services;

namespace SEP.Web.Controllers
{
	[AllowAnonymous]
	public class LoginController: Controller
    {
		private IEmployeeService employeeService;

        public LoginController()
        {
			employeeService = new EmployeeService();
        }

		public IActionResult Index()
        {
            return View();
        }

		[Route("/login")]
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> LoginAsync(string username, string password)
        {
			var employee = employeeService.ValidateCredential(username, password);

			if (employee != null)
			{
				//HttpContext.Session.SetString("username", username);

				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, employee.Username),
					new Claim(ClaimTypes.Name, employee.Username),
					new Claim("FullName", employee.Name),
					new Claim(ClaimTypes.Role, employee.Role.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. Required when setting the 
                    // ExpireTimeSpan option of CookieAuthenticationOptions 
                    // set with AddCookie. Also required when setting 
                    // ExpiresUtc.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
				
				return Redirect("/");
			}
			else
			{
				ViewBag.error = "Invalid Account";
                return View("Index");
			}           
        }

        [Route("logout")]
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> LogoutAsync()
        {
            //HttpContext.Session.Remove("username");
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			
            return RedirectToAction("Index");
        }
    }
}
