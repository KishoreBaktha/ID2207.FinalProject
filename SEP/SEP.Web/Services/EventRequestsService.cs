using System;
using SEP.Web.Models;
using SEP.Web.Repositories;

namespace SEP.Web.Services
{
	public interface IEventRequestsService
	{

		EventRequest CreateEventRequest(string eventname);
	}

	public class EventRequestsService: IEventRequestsService
    {
        public EventRequestsService()
        {
        }

        public EventRequest CreateEventRequest(string eventname)
		{
			var newEventRequest = new EventRequest
			{
				EventName = eventname
			};

			Database.EventRequests.Add(newEventRequest);
			return newEventRequest;
		}
    }
}
