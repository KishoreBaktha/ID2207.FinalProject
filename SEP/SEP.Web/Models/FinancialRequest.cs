 using System;
namespace SEP.Web.Models
{
	public enum FinancialRequestStatus
	{
        Pending,
		Approved,
        Rejected
	}

    public class FinancialRequest
    {
        public FinancialRequest()
        {
        }

		public string Id { get; set; }

		public string Department { get; set; }

		public Employee CreatedBy { get; set; }

		public DateTime CreatedAt { get; set; }

		public string EventRequestId { get; set; }

		public EventRequest EventRequest { get; set; }

		public int RequiredAmount { get; set; }

		public string Reason { get; set; }

		public FinancialRequestStatus Status { get; set; }
    }
}
