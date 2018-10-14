using System;
using System.Linq;
using System.Collections.Generic;
using SEP.Web.Models;
using SEP.Web.Repositories;

namespace SEP.Web.Services
{
	public interface IEventRequestsService
	{
		List<EventRequest> GetEventRequests();
		EventRequest GetEventRequestById(string eventRequestId);
		EventRequest CreateEventRequest(
			string clientname,
			string eventname,
			string eventtype, DateTime from, DateTime to,
			int attendance, bool decoration, bool food,
			bool filmphoto, bool music, bool poster, int budget);
		EventRequest UpdateEventRequest(string id, string clientName, string eventName,
		   string eventType, DateTime from, DateTime to,
		   int attendance, string decorationDescription,
		   string foodDescription, string filmPhotoDescription,
		   string musicDescription, string posterDescription,
            string computerissue, string extrarequirement, int budget);
		bool ApproveEventRequest(string id);
		bool RejectEventRequest(string id);
	}

	public class EventRequestsService : IEventRequestsService
	{
		private IUserContext userContext;

		public EventRequestsService(IUserContext userContext)
		{
			this.userContext = userContext;
		}

		public List<EventRequest> GetEventRequests()
		{
			return Database.EventRequests;
		}

		public EventRequest GetEventRequestById(string eventRequestId)
		{
			return Database.EventRequests.FirstOrDefault(x => x.Id == eventRequestId);
		}

		public EventRequest CreateEventRequest(
			string clientname,
			string eventname,
		    string eventtype, DateTime from, DateTime to, 
			int attendance, bool decoration, bool food, 
			bool filmphoto, bool music, bool poster, int budget)
		{
			var newEventRequest = new EventRequest
			{
				Id = Guid.NewGuid().ToString(),
				ClientName = clientname,
				EventName = eventname,
				EventType = eventtype,
                From = from,
                To = to,
				Attendance =attendance,
				Decoration = decoration,
                Food = food,
				FilmPhoto = filmphoto, 
				Music = music,
				Poster = poster,
				ExpectedBudget = budget,
				EventStatus = EventStatus.PendingApprovalFromSeniorCSO
			};

			Database.EventRequests.Add(newEventRequest);
			return newEventRequest;
		}

		public EventRequest UpdateEventRequest(string id, string clientName, string eventName,
            string eventType, DateTime from, DateTime to,
            int attendance, string decorationDescription,
            string foodDescription, string filmPhotoDescription,
            string musicDescription, string posterDescription,
            string computerissue, string extrarequirement, int budget)
		{
			var eventRequest = GetEventRequestById(id);
			if (eventRequest != null)
			{
				eventRequest.ClientName = clientName;
				eventRequest.EventName = eventName;
				eventRequest.EventType = eventType;
				eventRequest.Attendance = attendance;
				eventRequest.From = from;
				eventRequest.To = to;
				eventRequest.FoodDescription = foodDescription;
				eventRequest.DecorationDescription = decorationDescription;
				eventRequest.FilmPhotoDescription = filmPhotoDescription;
				eventRequest.MusicDescription = musicDescription;
				eventRequest.PosterDescription = posterDescription;
				eventRequest.ComputerIssue = computerissue;
				eventRequest.ExtraRequirement = extrarequirement;
				eventRequest.ExpectedBudget = budget;
				return eventRequest;
			}
			else
			{
				return null;
			}
		}

        public bool ApproveEventRequest(string id)
		{
			var eventRequest = GetEventRequestById(id);
			if (eventRequest != null)
			{
				switch(eventRequest.EventStatus) {
					case EventStatus.PendingApprovalFromSeniorCSO:
						if (userContext.CurrentUser.Role == EmployeeRole.SeniorCustomerService)
						{
							eventRequest.EventStatus = EventStatus.PendingApprovalFromFinance;
							return true;
						}
						else
							return false;						
					case EventStatus.PendingApprovalFromFinance:
						if (userContext.CurrentUser.Role == EmployeeRole.FinancialManager)
                        {
							eventRequest.EventStatus = EventStatus.PendingApprovalFromAdmin;
                            return true;
                        }
                        else
                            return false;
					case EventStatus.PendingApprovalFromAdmin:
						if (userContext.CurrentUser.Role == EmployeeRole.AdministrationManager)
                        {
							eventRequest.EventStatus = EventStatus.Open;
                            return true;
                        }
                        else
                            return false;
					default:
						return false;
				}
			}
			else
				return false;               
		}

        public bool RejectEventRequest(string id)
		{
			var eventRequest = GetEventRequestById(id);
            if (eventRequest != null)
            {
                switch(eventRequest.EventStatus) {
                    case EventStatus.PendingApprovalFromSeniorCSO:
                        if (userContext.CurrentUser.Role == EmployeeRole.SeniorCustomerService)
                        {
							eventRequest.EventStatus = EventStatus.Rejected;
                            return true;
                        }
                        else
                            return false;                       
                    case EventStatus.PendingApprovalFromFinance:
                        if (userContext.CurrentUser.Role == EmployeeRole.FinancialManager)
                        {
							eventRequest.EventStatus = EventStatus.Rejected;
                            return true;
                        }
                        else
                            return false;
                    case EventStatus.PendingApprovalFromAdmin:
                        if (userContext.CurrentUser.Role == EmployeeRole.AdministrationManager)
                        {
							eventRequest.EventStatus = EventStatus.Rejected;
                            return true;
                        }
                        else
                            return false;
                    default:
                        return false;
                }
            }
            else
                return false;        
		}
	}
}
