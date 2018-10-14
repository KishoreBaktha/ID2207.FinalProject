using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Models;
using SEP.Web.Services;

namespace SEP.Web.Controllers
{
	[Authorize]
	public class TasksController: BaseController
    {
		TasksService _tasksService;

		public TasksService tasksService
        {
            get
            {
				if (_tasksService == null)
                {
					_tasksService = new TasksService(CurrentUserContext);
                }

				return _tasksService;
            }
        }
              
		public IActionResult Index()
		{
			ViewData["Tasks"] = tasksService.GetTasksForCurrentUser();
			return View();
		}

		[Route("/tasks/create")]
		public IActionResult Create()
		{
			ViewData["DepartmentMembers"] = GetDepartmentMembers();
			ViewData["EventRequests"] = GetEventRequests();
			return View();
		}
                
        [Route("/tasks/create")]
        [HttpPost]
		public IActionResult Create(string name, string description, string eventRequestId, string priority, string assignedTo)
		{
			tasksService.CreateTask(assignedTo, eventRequestId, priority, description, name);
			return Redirect("/tasks");
		}


		[Route("/tasks/{taskId}")]
        public IActionResult ViewDetails(string taskId)
        {
			var task = tasksService.GetTaskById(taskId);

			if (task == null)
				return NotFound();
			
			ViewData["Task"] = task;
            return View();
        }

        [Route("/tasks/submitplan")]
        [HttpPost]
		public IActionResult SubmitPlan(string id, string activityplan)
		{
			tasksService.UpdateTaskActivityPlan(id, activityplan);                        
			return Redirect("/tasks");
		}

		[Route("/tasks/approve")]
        [HttpPost]
        public IActionResult ApproveTaskPlan(string id)
        {
			tasksService.ApproveTaskPlan(id);
            return Redirect("/tasks");
        }

		[Route("/tasks/close")]
        [HttpPost]
        public IActionResult CloseTask(string id)
        {
            tasksService.CloseTask(id);
            return Redirect("/tasks");
        }

		List<Employee> GetDepartmentMembers()
        {
            var employeeService = new EmployeeService();

            if (CurrentUserContext.CurrentUser.Role == EmployeeRole.ProducitonManager)
                return employeeService.GetEmployeesByRole(EmployeeRole.ProductionSubTeam);
            if (CurrentUserContext.CurrentUser.Role == EmployeeRole.ServicesManager)
                return employeeService.GetEmployeesByRole(EmployeeRole.ServiceSubTeam);

            return new List<Employee>();
        }

        List<EventRequest> GetEventRequests()
        {
            var eventRequestsService = new EventRequestsService(CurrentUserContext);
            return eventRequestsService.GetEventRequests();
        }
    }
}
