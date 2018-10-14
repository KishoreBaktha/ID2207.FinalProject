using System;
namespace SEP.Web.Models
{
	public enum TaskStatus 
	{
	    Assigned,
        PlanSubmitted,
        InProgress,
        Closed
	}

    public class Task
    {
        public Task()
        {
        }

		public string Id { get; set; }

		public string Name { get; set; }

		public Employee CreatedBy { get; set; }

		public DateTime CreatedAt { get; set; }

		public Employee AssignedMember { get; set; }

		public string EventRequestId { get; set; }

		public EventRequest EventRequest { get; set; }

		public string Priority { get; set; }

		public string Description { get; set; }

		public string ActivityPlan { get; set; }

		public TaskStatus Status { get; set; }
    }
}
