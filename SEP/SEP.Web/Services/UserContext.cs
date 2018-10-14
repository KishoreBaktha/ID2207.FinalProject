using System;
using Microsoft.AspNetCore.Http;
using SEP.Web.Models;

namespace SEP.Web.Services
{
	public interface IUserContext
	{
		Employee CurrentUser { get; set; }	
	}

	public class UserContext : IUserContext
	{
		public UserContext()
		{
		}

		public Employee CurrentUser { get; set; }        
    }
}
