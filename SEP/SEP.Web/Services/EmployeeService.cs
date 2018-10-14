using System;
using SEP.Web.Models;
using SEP.Web.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace SEP.Web.Services
{
	public interface IEmployeeService
    {
        Employee GetEmployee(string username);
        List<Employee> GetEmployeesByRole(EmployeeRole role);
        Employee ValidateCredential(string username, string password);
    }

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
		public List<Employee> GetEmployeesByRole(EmployeeRole role)
		{
			return Database.Employees.Where(x => x.Role == role).ToList();
		}
    }
}
