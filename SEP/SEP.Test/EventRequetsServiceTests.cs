using System;
using SEP.Web.Services;
using SEP.Web.Models;
using Xunit;
using SEP.Web.Repositories;

namespace SEP.Test
{
    public class EventRequestsServiceTests
    {
		static string EventPendingApprovalFromSeniorCSO = "27ce774e-beda-44e4-8b79-7c1529ecd6a4";
		static string EventPendingApprovalFromFinancialManager = "594a67f7-9982-4478-bb3d-36f9d66ae292";
		static string EventPendingApprovalFromAdminManager = "b5b93dbd-332a-4437-a12e-1ed7ebb010b3";
		static string EventOpen = "3ca95a5c-6741-46f9-ab6d-ce9c596ea6fa";
		static string EventInProgress = "c0c888de-9141-4cd1-931f-b86c14d3a8e5";
		static string EventRejected = "29383f76-4d48-4fd9-9acb-8536150bb844";

		static EventRequestsService eventRequestsService;
        
		[Fact]
		public void SeniorCustomerServiceApproveEvent()
        {
			SetupTestData(new Employee{
				Role = EmployeeRole.SeniorCustomerService
			});

			Assert.False(eventRequestsService.ApproveEventRequest(EventPendingApprovalFromFinancialManager));
			Assert.False(eventRequestsService.ApproveEventRequest(EventPendingApprovalFromAdminManager));
			Assert.False(eventRequestsService.ApproveEventRequest(EventOpen));
			Assert.False(eventRequestsService.ApproveEventRequest(EventInProgress));
			Assert.False(eventRequestsService.ApproveEventRequest(EventRejected));

			Assert.True(eventRequestsService.ApproveEventRequest(EventPendingApprovalFromSeniorCSO));
			var eventRequest = eventRequestsService.GetEventRequestById(EventPendingApprovalFromSeniorCSO);
			Assert.Equal(EventStatus.PendingApprovalFromFinance, eventRequest.EventStatus);            
        }

		[Fact]
        public void FinancialManagerApproveEvent()
        {
            SetupTestData(new Employee
            {
				Role = EmployeeRole.FinancialManager
            });

			Assert.False(eventRequestsService.ApproveEventRequest(EventPendingApprovalFromSeniorCSO));
            Assert.False(eventRequestsService.ApproveEventRequest(EventPendingApprovalFromAdminManager));
            Assert.False(eventRequestsService.ApproveEventRequest(EventOpen));
            Assert.False(eventRequestsService.ApproveEventRequest(EventInProgress));
            Assert.False(eventRequestsService.ApproveEventRequest(EventRejected));

			Assert.True(eventRequestsService.ApproveEventRequest(EventPendingApprovalFromFinancialManager));
			var eventRequest = eventRequestsService.GetEventRequestById(EventPendingApprovalFromFinancialManager);
			Assert.Equal(EventStatus.PendingApprovalFromAdmin, eventRequest.EventStatus);                       
        }

		[Fact]
        public void AdminManagerApproveEvent()
        {
            SetupTestData(new Employee
            {
				Role = EmployeeRole.AdministrationManager
            });

            Assert.False(eventRequestsService.ApproveEventRequest(EventPendingApprovalFromSeniorCSO));
			Assert.False(eventRequestsService.ApproveEventRequest(EventPendingApprovalFromFinancialManager));
            Assert.False(eventRequestsService.ApproveEventRequest(EventOpen));
            Assert.False(eventRequestsService.ApproveEventRequest(EventInProgress));
            Assert.False(eventRequestsService.ApproveEventRequest(EventRejected));

			Assert.True(eventRequestsService.ApproveEventRequest(EventPendingApprovalFromAdminManager));
			var eventRequest = eventRequestsService.GetEventRequestById(EventPendingApprovalFromAdminManager);
			Assert.Equal(EventStatus.Open, eventRequest.EventStatus);
        }

		private static void SetupTestData(Employee currentUser)
		{
			eventRequestsService = new EventRequestsService(new UserContext
            {
                CurrentUser = currentUser
            });

			Database.Initialize();

            Database.EventRequests.Add(new EventRequest
            {
                Id = EventPendingApprovalFromSeniorCSO,
                EventStatus = EventStatus.PendingApprovalFromSeniorCSO
            });

            Database.EventRequests.Add(new EventRequest
            {
                Id = EventPendingApprovalFromFinancialManager,
                EventStatus = EventStatus.PendingApprovalFromFinance
            });

			Database.EventRequests.Add(new EventRequest
            {
				Id = EventPendingApprovalFromAdminManager,
				EventStatus = EventStatus.PendingApprovalFromAdmin
            });

			Database.EventRequests.Add(new EventRequest
            {
				Id = EventOpen,
				EventStatus = EventStatus.Open
            });

			Database.EventRequests.Add(new EventRequest
            {
				Id = EventInProgress,
				EventStatus = EventStatus.InProgress
            });

			Database.EventRequests.Add(new EventRequest
            {
				Id = EventRejected,
				EventStatus = EventStatus.Rejected
            });
		}
    }
}
