using System;
using Microsoft.AspNetCore.Mvc;

namespace SEP.Web.Controllers
{
	public class TasksRequestsController: Controller
    {
		public TasksRequestsController()
        {
        }

		public IActionResult Index()
		{
			return View();
		}

		[Route("/tasks/create")]
		public IActionResult Create()
		{
			return View();
		}


		[Route("/tasks/viewdetails/{taskId}")]
        public IActionResult ViewDetails(int taskId)
        {
            return View();
        }
    }
}
