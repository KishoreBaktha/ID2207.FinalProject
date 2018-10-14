using System;
using SEP.Web.Models;
using SEP.Web.Repositories;
using SEP.Web.Services;

namespace SEP.Test
{
    public class TaskServiceTests
    {
		static string eventRequestId = Guid.NewGuid().ToString();
		static TasksService tasksService;
               
		static void SetupTestData(Employee currentUser)
        {
			tasksService = new TasksService(new UserContext
            {
                CurrentUser = currentUser
            });

            Database.Initialize();

            Database.EventRequests.Add(new EventRequest
            {
				Id = eventRequestId,
                EventStatus = EventStatus.Open
            });   
        }        
    }
}
