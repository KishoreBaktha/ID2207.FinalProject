using System;
namespace SEP.Web.Models
{
	public enum EventStatus
	{
        PendingApprovalFromSeniorCSO,
        PendingApprovalFromAdmin,
        PendingApprovalFromFinance,
        Open,
        InProgress,
        Closed,
        Rejected
	}

	public class EventRequest
	{
		public EventRequest()
		{
		}

		public Employee CreatedBy { get; set; }

		public string Id { get; set; }

		public string ClientName { get; set; }

		public string EventName { get; set; }

		public string EventType { get; set; }

		public string EventDescription { get; set; }

		public EventStatus EventStatus { get; set; }

		public DateTime From { get; set; }

		public DateTime To { get; set; }

		public int Attendance { get; set; }

		public bool Decoration { get; set; }

		public string DecorationDescription { get; set; }

		public bool Food { get; set; }

		public string FoodDescription { get; set; }

		public bool FilmPhoto { get; set; }

		public string FilmPhotoDescription { get; set; }

		public bool Music { get; set; }

		public string MusicDescription { get; set; }

		public bool Poster { get; set; }

		public string PosterDescription { get; set; }

		public string ComputerIssue { get; set; }

		public int ExpectedBudget { get; set; }

		public string ExtraRequirement { get; set; }
	}
}
