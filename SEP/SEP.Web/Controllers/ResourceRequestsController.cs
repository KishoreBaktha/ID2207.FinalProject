using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Models;
using SEP.Web.Services;

namespace SEP.Web.Controllers
{
	[Authorize]
	public class ResourceRequestsController: BaseController
    {
		IResourceRequestsService _resourceRequestsService;

		public IResourceRequestsService resourceRequestsService
        {
            get
            {
				if (_resourceRequestsService == null)
                {
					_resourceRequestsService = new ResourceRequestsService(CurrentUserContext);
                }

				return _resourceRequestsService;
            }
        }

		public IActionResult Index()
		{
			ViewData["CurrentUser"] = CurrentUserContext.CurrentUser;
			ViewData["ResourceRequests"] = resourceRequestsService.GetResourceRequests();
			return View();
		}

		[Route("/resourcerequests/create")]
		public IActionResult Create()
		{
			ViewData["CurrentUser"] = CurrentUserContext.CurrentUser;
			return View();
		}

        [Route("resourcerequests/create")]
        [HttpPost]
		public IActionResult Create(string department, ContractType contractType, int yearsOfExperience, string jobTitle, string jobDescription)
		{			
			resourceRequestsService.CreateResourceRequest(
				department, contractType, yearsOfExperience, jobTitle, jobDescription);
			return Redirect("/resourcerequests");
		}


		[Route("/resourcerequests/{resourceRequestId}")]
		public IActionResult ViewDetails(string resourceRequestId)
        {
			ViewData["CurrentUser"] = CurrentUserContext.CurrentUser;
			var resourceRequest = resourceRequestsService.GetResourceRequest(resourceRequestId);
			if (resourceRequest != null) 
			{
				ViewData["ResourceRequest"] = resourceRequest;
				return View();
			}
			else 
			{
				return NotFound();
			}
        }

        [Route("/resourcerequests/approve")]
		[HttpPost]
		public IActionResult Approve(string id)
		{
			resourceRequestsService.ApproveResourceRequest(id);
			return Redirect("/resourcerequests");
		}

		[Route("/resourcerequests/reject")]
        [HttpPost]
		public IActionResult Reject(string id)
		{
			resourceRequestsService.RejectResourceRequest(id);
            return Redirect("/resourcerequests");
		}
    }
}
