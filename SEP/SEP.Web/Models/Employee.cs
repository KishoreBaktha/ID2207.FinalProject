using System;
namespace SEP.Web.Models
{
	public enum EmployeeRole 
	{
        CustomerService,
        SeniorCustomerService,
        FinancialManager,
        AdministrationManager,
        ProducitonManager,
        ServicesManager,
        HRManager,
        ProductionSubTeam,
        ServiceSubTeam
	}

    public class Employee
    {
        public Employee()
        {
        }

		public string Name { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string Department { get; set; }

		public string DepartmentSubTeam { get; set; }

		public EmployeeRole Role { get; set; }
    }
}
