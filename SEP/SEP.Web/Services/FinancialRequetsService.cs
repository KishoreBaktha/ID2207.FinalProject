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
			var newFinancialRequest = new FinancialRequest { 
				Id = Guid.NewGuid().ToString(),
				Department = department,
				EventRequestId = eventRequestId,
				RequiredAmount = requiredAmount,
				Reason = reason,
				Status = FinancialRequestStatus.Pending,
				CreatedBy = this.userContext.CurrentUser,
				CreatedAt = DateTime.Now
			};

			Database.FinancialRequests.Add(newFinancialRequest);
			return newFinancialRequest;
		}
    }
}
