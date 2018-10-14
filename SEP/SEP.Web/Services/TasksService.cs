using System;
using System.Collections.Generic;
using System.Linq;
using SEP.Web.Models;
using SEP.Web.Repositories;

namespace SEP.Web.Services
{
	public interface ITasksService
	{
		Task GetTaskById(string id);
		List<Task> GetTasksForCurrentUser();
		List<Task> GetTaskCreatedBy(string username);
		List<Task> GetTaskAssignedTo(string username);
		Task CreateTask(string assignedMember, string eventRequestId, string priority, string description);
		Task UpdateTaskActivityPlan(string id, string activityPlan);
		Task ApproveTaskPlan(string id);
		Task CloseTask(string id);
	}

    public class TasksService
    {
		IUserContext userContext;

		public TasksService(IUserContext userContext)
        {
			this.userContext = userContext;
        }

        public Task GetTaskById(string id)
		{
			return Database.Tasks.FirstOrDefault(x => x.Id == id);
		}

        public List<Task> GetTasksForCurrentUser()
		{
			var tasks = GetTaskAssignedTo(userContext.CurrentUser.Username);
			tasks.AddRange(GetTaskCreatedBy(userContext.CurrentUser.Username));

			return tasks;
		}

        public List<Task> GetTaskCreatedBy(string username)
		{
			return Database.Tasks
				           .Where(x => x.CreatedBy.Username == username).ToList();

		}

        public List<Task> GetTaskAssignedTo(string username)
		{
			return Database.Tasks
				           .Where(x => x.AssignedMember.Username == username).ToList();
		}

        public Task CreateTask(string assignedMember, string eventRequestId, string priority, string description, string name)
		{
			var assignedEmployee = Database.Employees.FirstOrDefault(x => x.Username == assignedMember);
			var eventRequest = Database.EventRequests.FirstOrDefault(x => x.Id == eventRequestId);

			if (assignedMember == null)
				throw new Exception($"Cannot find user '{assignedMember}'");
			if (eventRequest == null)
				throw new Exception($"Cannot find event request with ID '{eventRequestId}'");

			if (userContext.CurrentUser.Role != EmployeeRole.ProducitonManager &&
				userContext.CurrentUser.Role != EmployeeRole.ServicesManager)
				throw new Exception("Only production manager or services manager can create task");

			if (userContext.CurrentUser.Role == EmployeeRole.ProducitonManager &&
				assignedEmployee.Role != EmployeeRole.ProductionSubTeam)
				throw new Exception("Production manager can only assign task to production department member");

			if (userContext.CurrentUser.Role == EmployeeRole.ServicesManager &&
			    assignedEmployee.Role != EmployeeRole.ServiceSubTeam)
                throw new Exception("Services manager can only assign task to services department member");

			var task = new Task
			{
				Id = Guid.NewGuid().ToString(),
				AssignedMember = assignedEmployee,
				EventRequestId = eventRequestId,
				EventRequest = eventRequest,
				Priority = priority,
                Name = name,
				Description = description,
				CreatedBy = userContext.CurrentUser,
                CreatedAt = DateTime.Now,
				Status = TaskStatus.Assigned
			};

			Database.Tasks.Add(task);
			return task;
		}

        public Task UpdateTaskActivityPlan(string id, string activityPlan)
		{
			var task = GetTaskById(id);
			if (task == null)
				throw new Exception($"Cannot find task with '{id}'");

			if (task.AssignedMember.Username != userContext.CurrentUser.Username)
				throw new Exception("Only assigned member can submit task activity plan");

			task.ActivityPlan = activityPlan;
			task.Status = TaskStatus.PlanSubmitted;

			return task;
		}

        public Task ApproveTaskPlan(string id)
		{
			var task = GetTaskById(id);
            if (task == null)
                throw new Exception($"Cannot find task with '{id}'");

			if (task.CreatedBy.Username != userContext.CurrentUser.Username)
				throw new Exception("Only task owner can approve task");

			task.Status = TaskStatus.InProgress;
			return task;
		}

        public Task CloseTask(string id)
		{
			var task = GetTaskById(id);
            if (task == null)
                throw new Exception($"Cannot find task with '{id}'");

            if (task.CreatedBy.Username != userContext.CurrentUser.Username &&
			    task.AssignedMember.Username != userContext.CurrentUser.Username)
                throw new Exception("Only task owner or assigned member can close task");

			task.Status = TaskStatus.Closed;
            return task;
		}
    }
}
