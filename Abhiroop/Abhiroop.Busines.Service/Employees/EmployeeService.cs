using Abhiroop.Domain.EmployeeDto;
using Abhiroop.Domain.Entities;
using Abhiroop.Repository;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Abhiroop.Busines.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;


        public EmployeeService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            this._unitOfWork = unitOfWork;
            this._contextAccessor = contextAccessor;
        }
        public async Task<Employee> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            try
            {
                var emplopyee = new Employee
                {
                    UserName = addEmployeeDto.UserName,
                    Address = addEmployeeDto.Address,
                    Email = addEmployeeDto.Email
                };

                var emp = await CheckEmailExist(emplopyee.Email) ? throw new Exception("Email Already Exist Before") : await _unitOfWork._abhiroopContext.AddAsync(emplopyee);

                return emplopyee;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Employee> DeleteEmployee(Employee employee)
        {
            try
            {
                _unitOfWork._abhiroopContext.Remove(employee);

                return await Task.FromResult(employee);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Employee> EditEmployee(EditEmployeeDto editEmployeeDto, Employee employee)
        {
            try
            {
                employee.Id = editEmployeeDto.Id;
                employee.Email = editEmployeeDto.Email;
                employee.Address = editEmployeeDto.Address;
                employee.UserName = editEmployeeDto.UserName;

                _unitOfWork._abhiroopContext.Update(employee);

                return await Task.FromResult(employee);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            try
            {
                var userId = _contextAccessor.HttpContext?.User.Claims?.FirstOrDefault(a => a.Type == "uid")?.Value ?? "";

                var employees = await _unitOfWork.EmployeeRepository.GetEmployees(a => true && a.Id != userId) ?? new List<Employee>();
                return employees;
            }
            catch
            {

                throw;
            }
        }

        public async Task<Employee> GetEmployee(Expression<Func<Employee, bool>> employee)
        {
            try
            {
                var employeeDb = await _unitOfWork.EmployeeRepository.GetEmployee(employee) ?? throw new Exception("Employee Not Found");
                return employeeDb;
            }
            catch
            {

                throw;
            }
        }

        #region PrivateMethod


        private async Task<bool> CheckEmailExist(string email)
        {
            var employees = await _unitOfWork.EmployeeRepository.GetEmployees(a => true);
            bool employeeExist = false;
            if (!string.IsNullOrWhiteSpace(email))
            {
                employeeExist = employees.Any(a => a.Email.ToLower().Trim() == email.ToLower().Trim());
            }
            return employeeExist;
        }
        #endregion
    }
}
