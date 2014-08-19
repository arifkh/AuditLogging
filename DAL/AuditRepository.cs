using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditDemo.ConsoleApplication.Models;
using AuditDemo.Core;

namespace AuditDemo.ConsoleApplication.DAL
{
    public class AuditRepository : IAuditRepository
    {
        private readonly EmploymentContext _context;

        public AuditRepository(EmploymentContext context)
        {
            this._context = context;
        }

        public IEnumerable<IAudit> GetAll()
        {
            var audits = this._context.Audits.ToList();
            return audits;
        }

        public void Add(IAudit auditEntityToAdd)
        {
            this._context.Audits.Add(new Audit
            {
                EntityName = auditEntityToAdd.EntityName,
                LogData = auditEntityToAdd.LogData,
                ModifiedDate = DateTime.Now,
                ModifiedBy = Environment.UserName
            });
        }
    }
}
