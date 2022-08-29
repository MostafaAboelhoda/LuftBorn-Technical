using Abhiroop.Domain.Context;
using Abhiroop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abhiroop.Repository.EmployeeRepositories
{
    public class EmployeeRepository
    {
        private readonly AbhiroopContext _abhiroopContext;

        public EmployeeRepository(AbhiroopContext abhiroopContext)
        {
            this._abhiroopContext = abhiroopContext;
        }

        public async Task<Employee> GetEmployee(Expression<Func<Employee, bool>> predicate)
        {
            return await _abhiroopContext.Employees?.FirstOrDefaultAsync(predicate);
        }
        public async Task<List<Employee>> GetEmployees(Expression<Func<Employee, bool>> predicate)
        {
            return await _abhiroopContext.Employees.Where(predicate).ToListAsync();
        }
    }
}
