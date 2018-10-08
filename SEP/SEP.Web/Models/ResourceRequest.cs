using System;
namespace SEP.Web.Models
{
	public enum ContractType 
	{
	    FullTime,
        PartTIme
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

		public Employee CreatedBy { get; set; }

		public ContractType ContractType { get; set; }

		public string RequestingDepartment { get; set; }

		public int YearsOfExperience { get; set; }

		public string JobTitle { get; set; }

		public string JobDescription { get; set; }

		public ResourceRequestStatus Status { get; set; }
    }
}
