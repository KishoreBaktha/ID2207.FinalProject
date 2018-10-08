using System;
using SEP.Web.Models;

namespace SEP.Web.Services
{
    public interface IEmployeeService
    {
		Employee GetEmployee(string username);
		Employee ValidateCredential(string username, string password);
    }
}
