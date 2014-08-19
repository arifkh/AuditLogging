using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditDemo.Core
{
    public interface IEmployee : IDescribable
    {
        int EmployeeID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
