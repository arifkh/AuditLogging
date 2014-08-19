using System;
using System.Collections.Generic;
using AuditDemo.Core;
using AuditDemo.ConsoleApplication.DAL;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace AuditDemo.ConsoleApplication.Controllers
{
    public class AuditController : IController
    {
        public AuditController()
        {
        }

        [Dependency]
        public IUnityContainer UnityContainer { get; set; }

        [Dependency]
        public IEmployeeAuditUnitOfWork EmployeeUnitOfWork { get; set; } 

        public void Activate()
        { 
        }

        public void ProcessCommand(string command)
        {
            switch (command.ToLower())
            {
                case "create":
                    DoCreateEmployee();
                    break;
                case "edit":
                    DoEditEmployee();
                    break;
                case "delete":
                    DoDeleteEmployee();
                    break;
                case "view":
                    DoShowAllEmployees();
                    break;
                case "logs":
                    DoShowAuditLogs();
                    break;
                default:
                    break;
            }
        }

        private void DoDeleteEmployee()
        {
            Console.WriteLine("Enter EmployeeId to delete");
            int employeeId = int.Parse(Console.ReadLine());

            this.EmployeeUnitOfWork.EmployeeRepository.Delete(employeeId);
            this.EmployeeUnitOfWork.Save();
        }

        private void DoShowAllEmployees()
        {
            var employees = EmployeeUnitOfWork.EmployeeRepository.GetAll();
            for (int i=0; i<employees.Count(); i++)
            {
                var employee = employees.ElementAt(i);
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("{0} => EmployeeId:{1} | FirstName:{2} | LastName:{3}",
                    i + 1, employee.EmployeeID, employee.FirstName, employee.LastName);
                
                if (employees.Count() == i+1)
                {
                    Console.WriteLine("-----------------------------------------------------");
                }
            }
        }

        private void DoShowAuditLogs()
        {
            var audits = EmployeeUnitOfWork.AuditRepository.GetAll();
            for (int i = 0; i < audits.Count(); i++)
            {
                var audit = audits.ElementAt(i);
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("{0} => AuditId:{1} | EntityName:{2} | ModifiedDate:{3} | ModifiedBy:{4}",
                    i + 1, audit.AuditId, audit.EntityName, audit.ModifiedDate, audit.ModifiedBy);
                Console.WriteLine(audit.LogData);

                if (audits.Count() == i+1)
                {
                    Console.WriteLine("-----------------------------------------------------");
                }
            }
        }

        private void DoEditEmployee()
        {
            Console.WriteLine("Enter EmployeeId to edit");
            int employeeId = int.Parse(Console.ReadLine());

            var employee = this.EmployeeUnitOfWork.EmployeeRepository.Get(employeeId);
            if (employee == null)
            {
                Console.WriteLine("Employee with id {0} not found", employeeId);
                return;
            }
            
            Console.WriteLine("Enter first name");
            Console.Write("Current value: {0} >> New Value: ", employee.FirstName);
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter last name");
            Console.Write("Current value: {0} >> New Value: ", employee.LastName);
            string lastName = Console.ReadLine();

            employee.FirstName = firstName;
            employee.LastName = lastName;

            this.EmployeeUnitOfWork.Save();
        }

        private void DoCreateEmployee()
        {
            Console.WriteLine("Enter First name");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Last name");
            string lastName = Console.ReadLine();

            dynamic objData = new JObject();
            objData.firstName = firstName;
            objData.lastName = lastName;
            string employeeData = objData.ToString();
            
            this.EmployeeUnitOfWork.EmployeeRepository.Add(employeeData);
            this.EmployeeUnitOfWork.Save();
        }
    }
}
