using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestUtilities.Core
{
    public interface IAuditable
    {
        PropertyInfo[] AuditableProperties { get; }
    }
}
