using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditDemo.Core
{
    public interface IEmployeeAuditUnitOfWork : IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }

        IAuditRepository AuditRepository { get; }
    }
}
