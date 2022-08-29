using Abhiroop.Domain.Context;
using Abhiroop.Repository.EmployeeRepositories;

namespace Abhiroop.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public AbhiroopContext _abhiroopContext { get; }
        public EmployeeRepository EmployeeRepository { get; }


        public UnitOfWork(AbhiroopContext abhiroopContext, EmployeeRepository employeeRepository)
        {
            this._abhiroopContext = abhiroopContext;
            this.EmployeeRepository = employeeRepository;
        }
        public async Task SaveChangesAysnc()
        {
            await _abhiroopContext.SaveChangesAsync();
        }
    }
}
