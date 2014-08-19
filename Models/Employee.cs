using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditDemo.Core;
using Newtonsoft.Json.Linq;

namespace AuditDemo.ConsoleApplication.Models
{
    [Auditable(AuditScope.ClassAndProperties)]
    public class Employee : IEmployee
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Describe()
        {
            dynamic json = new JObject();
            json.employeeId = this.EmployeeID;
            json.firstName = this.FirstName;
            json.lastName = this.LastName;

            return json.ToString();
        }
    }
}
