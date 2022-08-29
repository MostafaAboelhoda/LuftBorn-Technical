using Abhiroop.Domain.Context;
using Abhiroop.Repository.EmployeeRepositories;

namespace Abhiroop.Repository
{
    public interface IUnitOfWork
    {
        AbhiroopContext  _abhiroopContext { get; }

        EmployeeRepository EmployeeRepository { get; } 
        Task SaveChangesAysnc();

    }
}
