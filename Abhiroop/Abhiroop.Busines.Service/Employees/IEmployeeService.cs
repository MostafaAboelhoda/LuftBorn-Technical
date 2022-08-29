using Abhiroop.Domain.EmployeeDto;
using Abhiroop.Domain.Entities;
using System.Linq.Expressions;

namespace Abhiroop.Busines.Employees
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployee(AddEmployeeDto addEmployeeDto);
        Task<Employee> EditEmployee(EditEmployeeDto editEmployeeDto, Employee employee);
        Task<Employee> GetEmployee(Expression<Func<Employee, bool>> employee);
        Task<Employee> DeleteEmployee(Employee employee);
        Task<List<Employee>> GetAllEmployee();

    }
}
