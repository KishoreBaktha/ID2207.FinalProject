using System;
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
	}

	public class ResourceRequestsService: IResourceRequestsService
    {
		IUserContext userContext;

		public ResourceRequestsService(IUserContext userContext)
        {
			this.userContext = userContext;
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
				RequestingDepartment = department,
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
