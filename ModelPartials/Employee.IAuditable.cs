using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using TestUtilities.Core;

namespace TestUtilities.ConsoleApplication.Models
{
    public partial class Employee : IAuditable
    {
        public PropertyInfo[] AuditableProperties
        {
            get
            {
                // Only include properties of type int and string
                return this.GetType().GetProperties().Where(x => x.PropertyType == typeof(int) || x.PropertyType == typeof(string)).ToArray();
            }
        }
    }
}
