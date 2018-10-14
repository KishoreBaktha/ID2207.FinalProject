using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Models;
using SEP.Web.Services;

namespace SEP.Web.Controllers
{
	[Authorize]
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
			ViewData["EventRequests"] = GetEventRequests();
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

        [Route("/financialrequests/approve")]
        [HttpPost]
		public IActionResult ApproveFinancialRequest(string id)
		{
			financialRequestsService.ApproveFinancialRequest(id);
			return Redirect("/financialrequests");
		}

		[Route("/financialrequests/reject")]
        [HttpPost]
        public IActionResult RejectFinancialRequest(string id)
        {
            financialRequestsService.RejectFinancialRequest(id);
            return Redirect("/financialrequests");
        }

		List<EventRequest> GetEventRequests()
        {
            var eventRequestsService = new EventRequestsService(CurrentUserContext);
            return eventRequestsService.GetEventRequests();
        }
    }
}
