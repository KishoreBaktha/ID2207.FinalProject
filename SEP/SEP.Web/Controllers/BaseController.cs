using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP.Web.Services;

namespace SEP.Web.Controllers
{
	public class BaseController: Controller
	{
		private IUserContext _userContext;

        public IUserContext CurrentUserContext
		{
			get
			{
				if (_userContext == null)
				{
					var employeeService = new EmployeeService();
					_userContext = new UserContext
					{
						CurrentUser = employeeService.GetEmployee(HttpContext.User.Identity.Name)
					};
					ViewData["CurrentUser"] = _userContext.CurrentUser;                   
				}
                
				return _userContext;
			}
		}
    }
}
