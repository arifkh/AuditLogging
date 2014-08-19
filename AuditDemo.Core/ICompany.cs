using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditDemo.Core
{
    public interface ICompany : IDescribable
    {
        int CompanyID { get; set; }
        string CompanyName { get; set; }
        string Country { get; set; }
    }
}
