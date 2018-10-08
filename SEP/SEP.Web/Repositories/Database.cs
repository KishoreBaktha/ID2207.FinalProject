using System;
using System.Collections.Generic;
using SEP.Web.Models;

namespace SEP.Web.Repositories
{
	public static class Database
	{
		public static List<Employee> Employees {get;set;}

		public static List<EventRequest> EventRequests { get; set; }
        
		public static List<FinancialRequest> FinancialRequests { get; set; }

		public static List<ResourceRequest> ResourceRequests { get; set; }

		public static List<Task> Tasks { get; set; }

        static Database()
        {
			Initialize();
        }

        public static void Initialize()
		{
			EventRequests = new List<EventRequest>();
            FinancialRequests = new List<FinancialRequest>();
            ResourceRequests = new List<ResourceRequest>();
            Tasks = new List<Task>();

            Employees = new List<Employee> {
                new Employee {
                    Name = "Mike",
                    Username = "adn_mike",
                    Password = "123",
                    Department = "Administration",
                    DepartmentSubTeam = "",
                    Role = EmployeeRole.AdministrationManager
                },
                new Employee {
                    Name = "Janet",
                    Username = "scs_janet",
                    Password = "123",
                    Department = "Administration",
                    DepartmentSubTeam = "",
                    Role = EmployeeRole.SeniorCustomerService
                },
                new Employee {
                    Name = "Sarah",
                    Username = "cso_sarah",
                    Password = "123",
                    Department = "Administration",
                    DepartmentSubTeam = "",
                    Role = EmployeeRole.CustomerService
                },
                new Employee {
                    Name = "Simon",
                    Username = "hrm_simon",
                    Password = "123",
                    Department = "Administration",
                    DepartmentSubTeam = "",
                    Role = EmployeeRole.HRManager
                },
                new Employee {
                    Name = "Alice",
                    Username = "fim_alice",
                    Password = "123",
                    Department = "Financial",
                    DepartmentSubTeam = "",
                    Role = EmployeeRole.FinancialManager
                },
                new Employee {
                    Name = "Jack",
                    Username = "pro_jack",
                    Password = "123",
                    Department = "Production",
                    DepartmentSubTeam = "",
                    Role = EmployeeRole.ProducitonManager
                },
                new Employee {
                    Name = "Tobias",
                    Username = "pho_tobias",
                    Password = "123",
                    Department = "Production",
                    DepartmentSubTeam = "Photographer",
                    Role = EmployeeRole.ProductionSubTeam
                },
                new Employee {
                    Name = "Antony",
                    Username = "aud_antony",
                    Password = "123",
                    Department = "Production",
                    DepartmentSubTeam = "Audio",
                    Role = EmployeeRole.ProductionSubTeam
                },
                new Employee {
                    Name = "Julia",
                    Username = "gra_julia",
                    Password = "123",
                    Department = "Production",
                    DepartmentSubTeam = "Graphic",
                    Role = EmployeeRole.ProductionSubTeam
                },
                new Employee {
                    Name = "Natalie",
                    Username = "srm_natalie",
                    Password = "123",
                    Department = "Service",
                    DepartmentSubTeam = "",
                    Role = EmployeeRole.ServicesManager
                },
                new Employee {
                    Name = "Helen",
                    Username = "che_helen",
                    Password = "123",
                    Department = "Service",
                    DepartmentSubTeam = "Chef",
                    Role = EmployeeRole.ServiceSubTeam
                },
                new Employee {
                    Name = "Kate",
                    Username = "wtr_kate",
                    Password = "123",
                    Department = "Service",
                    DepartmentSubTeam = "Waitress",
                    Role = EmployeeRole.ServiceSubTeam
                },
            };
		}
    }
}
