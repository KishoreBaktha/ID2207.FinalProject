using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Services;

namespace SEP.Web.Controllers
{
    public class EventRequestsController : Controller
    {
        IEventRequestsService eventRequestsService;

        public EventRequestsController()
        {
			var employeeService = new EmployeeService();
			eventRequestsService = new EventRequestsService(new UserContext {
				CurrentUser = employeeService.GetEmployee(HttpContext.Session.GetString("username"))
			});
        }

        public IActionResult Index()
        {
			ViewData["EventRequests"] = eventRequestsService.GetEventRequests();
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
			return Redirect("/eventrequests");

        }

		[Route("/eventrequests/update")]
        [HttpPost]
		public IActionResult Update(string id, string eventName)
		{
			eventRequestsService.UpdateEventRequest(id, eventName);
			return Redirect("/eventrequests");
		}

        [Route("/eventrequests/{eventRequestId}")]
        public IActionResult ViewDetails(string eventRequestId)
        {
			var eventRequest = eventRequestsService.GetEventRequestById(eventRequestId);
			if (eventRequest == null)
			{
				// return 404 or something
				return Ok();
			}
			else 
			{
				ViewData["EventRequest"] = eventRequest;
				return View();
			}            
        }
    }
}
