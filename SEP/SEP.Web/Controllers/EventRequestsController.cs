using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Services;

namespace SEP.Web.Controllers
{
	[Authorize]
	public class EventRequestsController : BaseController
    {
        IEventRequestsService _eventRequestsService;

		protected IEventRequestsService eventRequestsService
		{
			get
			{
				if (_eventRequestsService == null)
				{
					_eventRequestsService = new EventRequestsService(CurrentUserContext);
				}

				return _eventRequestsService;
			}
		}

        public IActionResult Index()
        {
			ViewData["EventRequests"] = eventRequestsService.GetEventRequests();
			ViewData["CurrentUser"] = CurrentUserContext.CurrentUser;
            return View();
        }

        [Route("/eventrequests/create")]
        public IActionResult Create()
        {
			ViewData["CurrentUser"] = CurrentUserContext.CurrentUser;
			ViewData["Clients"] = new List<string> { 
				"Company A",
                "Company B",
                "Company C"
			};
            return View();
        }

        [Route("/eventrequests/create")]
        [HttpPost]
        public IActionResult Create(
			string clientname, 
			string eventname, 
			string eventtype, 
			DateTime from, 
			DateTime to, 
			int attendance, 
		    bool decoration,bool food, bool filmphoto, bool music, bool poster, int budget)
        {
			eventRequestsService.CreateEventRequest(
				clientname, eventname, eventtype, 
				from, to, attendance, 
				decoration, food, filmphoto, music, poster, 
				budget);
			
			return Redirect("/eventrequests");
        }

		[Route("/eventrequests/update")]
        [HttpPost]
		public IActionResult Update(
			string id, string clientName, string eventName,
            string eventType, DateTime from, DateTime to,
		    int attendance, string decorationDescription,
		    string foodDescription, string filmPhotoDescription, 
			string musicDescription, string posterDescription, 
			string computerissue, string extrarequirement, int budget)
		{
			eventRequestsService.UpdateEventRequest(
				id, 
                clientName,
				eventName,
				eventType, 
				from, to,
				attendance, decorationDescription,
				foodDescription,
				filmPhotoDescription,
				musicDescription, 
				posterDescription,
				computerissue, 
				extrarequirement,
				budget);
			return Redirect("/eventrequests");
		}

        [Route("/eventrequests/{eventRequestId}")]
        public IActionResult ViewDetails(string eventRequestId)
        {
			ViewData["CurrentUser"] = CurrentUserContext.CurrentUser;
			var eventRequest = eventRequestsService.GetEventRequestById(eventRequestId);
			if (eventRequest == null)
				return NotFound();

			ViewData["EventRequest"] = eventRequest;
			return View();
        }
    }
}
