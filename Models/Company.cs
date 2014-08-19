using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using AuditDemo.Core;
using Newtonsoft.Json.Linq;

namespace AuditDemo.ConsoleApplication.Models
{
    [Auditable(AuditScope.ClassOnly)]
    public class Company : ICompany
    {
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string Country { get; set; }

        public string Describe()
        {
            dynamic json = new JObject();
            json.companyId = this.CompanyID;
            json.companyName = this.CompanyName;
            json.country = this.Country;

            return json.ToString();
        }
    }
}
