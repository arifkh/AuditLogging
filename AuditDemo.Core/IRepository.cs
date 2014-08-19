using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditDemo.Core
{
    public interface IRepository
    {
        void Add(string data);
        void Delete(int id);
    }

    public interface IRepository<T> : IRepository
    {
        T Get(int id);

        IEnumerable<T> GetAll();
    }
}
