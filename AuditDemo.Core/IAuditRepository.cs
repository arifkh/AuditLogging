using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditDemo.Core
{
    public interface IAuditRepository
    {
        IEnumerable<IAudit> GetAll();

        void Add(IAudit auditEntityToAdd);
    }
}
