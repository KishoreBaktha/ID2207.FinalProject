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
		EventRequest CreateEventRequest(string eventname);
		EventRequest UpdateEventRequest(string id, string eventName);
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

		public EventRequest CreateEventRequest(string eventname)
		{
			var newEventRequest = new EventRequest
			{
				Id = Guid.NewGuid().ToString(),
				EventName = eventname
			};

			Database.EventRequests.Add(newEventRequest);
			return newEventRequest;
		}

		public EventRequest UpdateEventRequest(string id, string eventName)
		{
			var eventRequest = GetEventRequestById(id);
			if (eventRequest != null)
			{
				eventRequest.EventName = eventName;
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
