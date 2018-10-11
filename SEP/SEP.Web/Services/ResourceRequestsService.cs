using System;
using System.Collections.Generic;
using System.Linq;
using SEP.Web.Models;
using SEP.Web.Repositories;

namespace SEP.Web.Services
{
	public interface IResourceRequestsService
	{
		ResourceRequest CreateResourceRequest(
			string department,
			ContractType contractType,
			int experience,
			string jobTitle,
			string jobDescription);

		List<ResourceRequest> GetResourceRequests();
		ResourceRequest GetResourceRequest(string id);
	}

	public class ResourceRequestsService: IResourceRequestsService
    {
		IUserContext userContext;

		public ResourceRequestsService(IUserContext userContext)
        {
			this.userContext = userContext;
        }

		public List<ResourceRequest> GetResourceRequests()
        {
			if (userContext.CurrentUser.Role == EmployeeRole.HRManager)
				return Database.ResourceRequests;

			return Database.ResourceRequests
                           .Where(x => x.CreatedBy == userContext.CurrentUser)
                           .ToList();
        }

		public ResourceRequest GetResourceRequest(string id)
        {
			return Database.ResourceRequests.FirstOrDefault(x => x.Id == id);
        }

		public ResourceRequest CreateResourceRequest(
			string department, 
			ContractType contractType, 
			int experience, 
			string jobTitle, 
			string jobDescription)
		{
			var newResourceRequest = new ResourceRequest { 
				Id = Guid.NewGuid().ToString(),
				Department = department,
				YearsOfExperience = experience,
				ContractType = contractType,
				JobTitle = jobTitle,
				JobDescription = jobDescription,
				CreatedBy = userContext.CurrentUser,
				CreatedAt = DateTime.Now,
				Status = ResourceRequestStatus.Pending
			};

			Database.ResourceRequests.Add(newResourceRequest);
			return newResourceRequest;
		}
    }
}
