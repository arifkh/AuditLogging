using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditDemo.Core
{
    public interface IEmployeeRepository : IRepository<IEmployee>
    {
    }
}
