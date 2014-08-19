using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditDemo.ConsoleApplication.Models;
using AuditDemo.Core;

namespace AuditDemo.ConsoleApplication.DAL
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly EmploymentContext _context;

        public CompanyRepository(EmploymentContext context)
        {
            this._context = context;
        }

        public ICompany Get(int companyId)
        {
            var company = _context.Companies.FirstOrDefault(x => x.CompanyID == companyId);
            return company;
        }

        public void Add(string jsonData)
        {
            throw new NotImplementedException();
        }

        public void Delete(int companyId)
        {
            var company = Get(companyId) as Company;
            if (company != null)
            {
                _context.Companies.Remove(company);
            }
        }


        public IEnumerable<ICompany> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
