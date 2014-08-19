using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditDemo.Core;

namespace AuditDemo.ConsoleApplication.Models
{
    public partial class Audit : IAudit
    {
        public int AuditId { get; set; }

        public string EntityName { get; set; }

        public string LogData { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
