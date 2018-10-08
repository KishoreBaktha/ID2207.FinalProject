using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Services;

namespace SEP.Web.Controllers
{
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
        public IActionResult Login(string username, string password)
        {
			var employee = employeeService.ValidateCredential(username, password);

			if (employee != null)
			{
				HttpContext.Session.SetString("username", username);
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
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}
