using System;
using System.Collections.Generic;
using System.Linq;
using SEP.Web.Models;
using SEP.Web.Repositories;

namespace SEP.Web.Services
{
	public interface IFinancialRequestsService
	{
		List<FinancialRequest> GetFinancialRequests();
		FinancialRequest GetFinancialRequest(string id);
		FinancialRequest CreateFinancialRequest(string department, string eventRequestId, int requiredAmount, string reason);
		FinancialRequest ApproveFinancialRequest(string id);
		FinancialRequest RejectFinancialRequest(string id);
	}

	public class FinancialRequetsService: IFinancialRequestsService
    {
		IUserContext userContext;

        public FinancialRequetsService(IUserContext userContext)
        {
			this.userContext = userContext;
        }

        public List<FinancialRequest> GetFinancialRequests()
		{
			if (userContext.CurrentUser.Role == EmployeeRole.FinancialManager)
				return Database.FinancialRequests;
			
			return Database.FinancialRequests
				           .Where(x => x.CreatedBy == userContext.CurrentUser)
				           .ToList();
		}

        public FinancialRequest GetFinancialRequest(string id)
		{
			return Database.FinancialRequests.FirstOrDefault(x => x.Id == id);
		}

        public FinancialRequest CreateFinancialRequest(
			string department, 
			string eventRequestId, 
			int requiredAmount, 
			string reason)
		{
			var eventRequest = Database.EventRequests.FirstOrDefault(x => x.Id == eventRequestId);

			if (eventRequest == null)
                throw new Exception($"Cannot find event request with ID '{eventRequestId}'");
			
			var newFinancialRequest = new FinancialRequest { 
				Id = Guid.NewGuid().ToString(),
				Department = department,
				EventRequestId = eventRequestId,
				EventRequest = eventRequest,
				RequiredAmount = requiredAmount,
				Reason = reason,
				Status = FinancialRequestStatus.Pending,
				CreatedBy = this.userContext.CurrentUser,
				CreatedAt = DateTime.Now
			};

			Database.FinancialRequests.Add(newFinancialRequest);
			return newFinancialRequest;
		}

		public FinancialRequest ApproveFinancialRequest(string id)
        {
			var financialRequest = GetFinancialRequest(id);
			if (financialRequest == null)
                throw new Exception($"Cannot find financial request with '{id}'");

			if (userContext.CurrentUser.Role != EmployeeRole.FinancialManager)
                throw new Exception("Only financial manager can approve financial request");

			financialRequest.Status = FinancialRequestStatus.Approved;
			return financialRequest;
        }

		public FinancialRequest RejectFinancialRequest(string id)
        {
            var financialRequest = GetFinancialRequest(id);
            if (financialRequest == null)
                throw new Exception($"Cannot find financial request with '{id}'");

            if (userContext.CurrentUser.Role != EmployeeRole.FinancialManager)
                throw new Exception("Only financial manager can reject financial request");

			financialRequest.Status = FinancialRequestStatus.Rejected;
            return financialRequest;
        }
    }
}
