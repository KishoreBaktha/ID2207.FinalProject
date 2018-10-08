using System;
using Microsoft.AspNetCore.Mvc;

namespace SEP.Web.Controllers
{
	public class ResourceRequestsController: Controller
    {
		public ResourceRequestsController()
        {
        }

		public IActionResult Index()
		{
			return View();
		}

		[Route("/resourcerequests/create")]
		public IActionResult Create()
		{
			return View();
		}


		[Route("/resourcerequests/viewdetails/{resourceRequestId}")]
		public IActionResult ViewDetails(int resourceRequestId)
        {
            return View();
        }
    }
}
