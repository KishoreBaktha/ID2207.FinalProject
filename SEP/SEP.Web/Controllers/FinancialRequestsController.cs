using System;
using Microsoft.AspNetCore.Mvc;

namespace SEP.Web.Controllers
{
	public class FinancialRequestsController: Controller
    {
		public FinancialRequestsController()
        {
        }

		public IActionResult Index()
		{
			return View();
		}

		[Route("/financialrequests/create")]
		public IActionResult Create()
		{
			return View();
		}


		[Route("/financialrequests/viewdetails/{financialRequestId}")]
		public IActionResult ViewDetails(int financialRequestId)
        {
            return View();
        }
    }
}
