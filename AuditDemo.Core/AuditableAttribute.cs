using System;

namespace AuditDemo.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuditableAttribute : Attribute
    {
        private AuditScope _auditScope = AuditScope.ClassOnly;

        public AuditableAttribute(AuditScope auditScope)
        {
            this._auditScope = auditScope;
        }

        public AuditScope AuditScope
        {
            get { return _auditScope; }
        }
    }
}
