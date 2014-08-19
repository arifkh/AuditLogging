using AuditDemo.ConsoleApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditDemo.Core;
using System.Data.Entity.Infrastructure;

namespace AuditDemo.ConsoleApplication.DAL
{
    public class EmploymentContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Audit> Audits { get; set; }
    }
}
