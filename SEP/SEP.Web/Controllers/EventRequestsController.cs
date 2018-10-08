using System;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Services;

namespace SEP.Web.Controllers
{
	public class EventRequestsController: Controller
    {
		IEventRequestsService eventRequestsService;

		public EventRequestsController()
        {
			eventRequestsService = new EventRequestsService();				
        }

		public IActionResult Index()
        {
            return View();
        }

        [Route("/eventrequests/create")]
		public IActionResult Create()
		{
			return View();
		}
        
		[Route("/eventrequests/create")]
		[HttpPost]
        public IActionResult Create(string eventname)
        {
				eventRequestsService.CreateEventRequest(eventname);
                return Ok();

        }

		[Route("/eventrequests/view/{eventRequestId}")]
		public IActionResult ViewDetails(int eventRequestId)
		{
			return View();
		}
    }
}
