using Abhiroop.Busines.Employees;
using Abhiroop.Domain.EmployeeDto;
using Abhiroop.Repository;

namespace Abhiroop.Busines.Control.Employees
{
    public class EmployeeBusinessManager
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeBusinessManager(IEmployeeService employeeService, IUnitOfWork unitOfWork)
        {
            this._employeeService = employeeService;
            this._unitOfWork = unitOfWork;
        }

        public async Task<GetEmployeeDto> AddEmployeeAysnc(AddEmployeeDto addEmployeeDto)
        {
            var employee = await _employeeService.AddEmployee(addEmployeeDto);

            await _unitOfWork.SaveChangesAysnc();

            return new GetEmployeeDto
            {
                Id = employee.Id,
                Email = employee.Email,
                Address = employee.Address,
                UserName = employee.UserName
            };
        }
        public async Task<GetEmployeeDto> EditEmployeeAysnc(EditEmployeeDto editEmployeeDto)
        {
            var oldEmployee = await _employeeService.GetEmployee(a => a.Id == editEmployeeDto.Id);

            var employee = await _employeeService.EditEmployee(editEmployeeDto, oldEmployee);

            await _unitOfWork.SaveChangesAysnc();

            return new GetEmployeeDto
            {
                Id = employee.Id,
                Email = employee.Email,
                Address = employee.Address,
                UserName = employee.UserName
            };
        }

        public async Task<GetEmployeeDto> GetEmployeeAysnc(string id)
        {
            var employee = await _employeeService.GetEmployee(a => a.Id == id);

            return new GetEmployeeDto
            {
                Id = employee.Id,
                Email = employee.Email,
                Address = employee.Address,
                UserName = employee.UserName
            };
        }
        public async Task<List<GetEmployeeDto>> GetAllEmployeesAysnc()
        {
            var employees = await _employeeService.GetAllEmployee();

            return employees.Select(a => new GetEmployeeDto
            {
                Id = a.Id,
                Email = a.Email,
                UserName = a.UserName,
                Address = a.Address
            }).ToList();
        }

        public async Task<GetEmployeeDto> DeleteEmployeeAysnc(string id)
        {
            var employee = await _employeeService.GetEmployee(a => a.Id == id);

            await _employeeService.DeleteEmployee(employee);
            await _unitOfWork.SaveChangesAysnc();

            return new GetEmployeeDto
            {
                Id = employee.Id,
                Email = employee.Email,
                Address = employee.Address,
                UserName = employee.UserName
            };
        }
    }
}
