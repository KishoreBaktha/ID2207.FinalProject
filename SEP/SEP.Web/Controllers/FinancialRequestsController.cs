using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Services;

namespace SEP.Web.Controllers
{
	public class FinancialRequestsController: BaseController
    {
		IFinancialRequestsService _financialRequestsService;

		public IFinancialRequestsService financialRequestsService
		{
			get
			{
				if (_financialRequestsService == null)
				{
					_financialRequestsService = new FinancialRequetsService(CurrentUserContext);
				}

				return _financialRequestsService;
			}
		}

		public IActionResult Index()
		{
			ViewData["FinancialRequests"] = financialRequestsService.GetFinancialRequests();
			return View();
		}

		[Route("/financialrequests/create")]
		public IActionResult Create()
		{			
			return View();
		}
        
        [Route("/financialrequests/create")]
        [HttpPost]
        public IActionResult Create(string department, string eventRequestId, int requiredAmount, string reason)
		{
			financialRequestsService.CreateFinancialRequest(department, eventRequestId, requiredAmount, reason);
			return Redirect("/financialrequests");
		}


		[Route("/financialrequests/{financialRequestId}")]
		public IActionResult ViewDetails(string financialRequestId)
        {
			var financialRequest = financialRequestsService.GetFinancialRequest(financialRequestId);
			if (financialRequest == null)
			{
				return NotFound();
			}

			ViewData["FinancialRequest"] = financialRequest;
			return View();	
        }
    }
}
