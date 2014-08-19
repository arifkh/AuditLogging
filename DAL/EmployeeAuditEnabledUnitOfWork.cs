using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using AuditDemo.Core;
using AuditDemo.ConsoleApplication.Models;
using Newtonsoft.Json.Linq;

namespace AuditDemo.ConsoleApplication.DAL
{
    public class EmployeeAuditEnabledUnitOfWork : IEmployeeAuditUnitOfWork
    {
        private EmploymentContext _context;

        public EmployeeAuditEnabledUnitOfWork(IEmployeeRepository employeeRepository, IAuditRepository auditRepository, EmploymentContext context)
        {
            this.EmployeeRepository = employeeRepository;
            this.AuditRepository = auditRepository;

            this._context = context;
        }

        public IEmployeeRepository EmployeeRepository { get; private set; }

        public IAuditRepository AuditRepository { get; private set; }

        public void Save()
        {
            foreach (DbEntityEntry dbEntry in _context.ChangeTracker.Entries().Where(
                x => x.State == EntityState.Added || x.State == EntityState.Deleted || x.State == EntityState.Modified))
            {
                Type employeeType = dbEntry.State == EntityState.Deleted 
                    ? dbEntry.OriginalValues.ToObject().GetType() 
                    : dbEntry.CurrentValues.ToObject().GetType();
                AuditableAttribute auditableAttr = employeeType.GetCustomAttribute<AuditableAttribute>();

                if (auditableAttr == null)
                {
                    continue;
                }

                if (auditableAttr.AuditScope == AuditScope.ClassOnly || auditableAttr.AuditScope == AuditScope.ClassAndProperties)
                {
                    if (dbEntry.State == EntityState.Added || dbEntry.State == EntityState.Deleted)
                    {
                        var describable = dbEntry.Entity as IDescribable;
                        if (describable != null)
                        {
                            dynamic auditLog = new JObject();
                            auditLog.state = dbEntry.State.ToString();
                            auditLog.data = describable.Describe();

                            var auditEntry = new Audit
                            {
                                EntityName = dbEntry.Entity.GetType().Name,
                                LogData = auditLog.ToString()
                            };

                            this.AuditRepository.Add(auditEntry);
                        }
                    }
                }
                if (auditableAttr.AuditScope == AuditScope.PropertiesOnly || auditableAttr.AuditScope == AuditScope.ClassAndProperties)
                {
                    if (dbEntry.State == EntityState.Modified)
                    {
                        foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                        {
                            // For updates, we only want to capture the columns that actually changed
                            var origValue = dbEntry.OriginalValues.GetValue<object>(propertyName);
                            var newValue = dbEntry.CurrentValues.GetValue<object>(propertyName);
                            if (!object.Equals(origValue, newValue))
                            {
                                dynamic auditLog = new JObject();
                                auditLog.propertyName = propertyName;
                                auditLog.origValue = origValue;
                                auditLog.newValue = newValue;

                                var auditEntry = new Audit
                                {
                                    EntityName = dbEntry.Entity.GetType().Name,
                                    LogData = auditLog.ToString()
                                };

                                this.AuditRepository.Add(auditEntry);
                            }
                        }
                    }
                }
            }

            // In all versions of Entity Framework, whenever you execute SaveChanges() to insert, 
            // update or delete on the database the framework will wrap that operation in a transaction. 
            // This transaction lasts only long enough to execute the operation and then completes. 
            // When you execute another such operation a new transaction is started.

            _context.SaveChanges();
        }

        #region IDisposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
