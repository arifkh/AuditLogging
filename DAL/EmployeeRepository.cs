using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditDemo.ConsoleApplication.Models;
using AuditDemo.Core;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace AuditDemo.ConsoleApplication.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmploymentContext _context;

        public EmployeeRepository(EmploymentContext context)
        {
            this._context = context;
        }

        public IEmployee Get(int employeeId)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeID == employeeId);
            return employee;
        }

        public IEnumerable<IEmployee> GetAll()
        {
            var employees = _context.Employees.ToList();
            return employees;
        }

        public void Add(string employeeData)
        {
            var jObject = JObject.Parse(employeeData);
            string firstName = ((dynamic)jObject).firstName;
            string lastName = ((dynamic)jObject).lastName;

            _context.Employees.Add(new Employee
            {
                FirstName = firstName,
                LastName = lastName
            });
        }

        public void Delete(int employeeId)
        {
            var employee = Get(employeeId) as Employee; 
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
        }
    }
}
