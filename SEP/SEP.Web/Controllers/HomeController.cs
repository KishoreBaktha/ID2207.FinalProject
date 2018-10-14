using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Models;

namespace SEP.Web.Controllers
{
	[Authorize]
	public class HomeController : BaseController
    {
        public IActionResult Index()
        {
			ViewData["CurrentUser"] = CurrentUserContext.CurrentUser;
            return View();
        }              

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {			
			ViewData["CurrentUser"] = CurrentUserContext.CurrentUser;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
