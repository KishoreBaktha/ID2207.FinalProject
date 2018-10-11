using System;
namespace SEP.Web.Models
{
	public enum ContractType 
	{
	    FullTime,
        PartTime
	}

    public enum ResourceRequestStatus
	{
        Pending,
        Approved,
        Rejected
	}

    public class ResourceRequest
    {
        public ResourceRequest()
        {
        }

		public string Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public Employee CreatedBy { get; set; }

		public ContractType ContractType { get; set; }

		public string Department { get; set; }

		public int YearsOfExperience { get; set; }

		public string JobTitle { get; set; }

		public string JobDescription { get; set; }

		public ResourceRequestStatus Status { get; set; }
    }
}
