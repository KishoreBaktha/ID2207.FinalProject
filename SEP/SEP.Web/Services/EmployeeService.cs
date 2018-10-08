using System;
using SEP.Web.Models;
using SEP.Web.Repositories;
using System.Linq;

namespace SEP.Web.Services
{
	public class EmployeeService: IEmployeeService
    {
        public EmployeeService()
        {
        }

        public Employee ValidateCredential(string username, string password)
		{
			return Database
				.Employees
				.FirstOrDefault(e => 
				                e.Username == username && 
				                e.Password == password);
		}

        public Employee GetEmployee(string username)
		{
			return Database
                .Employees
                .FirstOrDefault(e =>
                                e.Username == username);
            
		}
    }
}
