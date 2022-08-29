using Abhiroop.Busines.Control.Employees;
using Abhiroop.Domain.EmployeeDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abhiroop.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeBusinessManager _employeeBusinessManager;
        public EmployeesController(EmployeeBusinessManager employeeBusinessManager)
        {
            this._employeeBusinessManager = employeeBusinessManager;
        }

        [HttpPost("addEmployee")]
        public async Task<ActionResult<GetEmployeeDto>> AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            var employee = await _employeeBusinessManager.AddEmployeeAysnc(addEmployeeDto);
            return Ok(employee);
        }

        [HttpPost("editEmployee")]
        public async Task<ActionResult<GetEmployeeDto>> EditEmployee([FromBody] EditEmployeeDto editEmployeeDto)
        {
            var employee = await _employeeBusinessManager.EditEmployeeAysnc(editEmployeeDto);
            return Ok(employee);
        }

        [HttpGet("getEmployee/{id}")]
        public async Task<ActionResult<GetEmployeeDto>> GetEmployee(string id)
        {
            var employee = await _employeeBusinessManager.GetEmployeeAysnc(id);
            return Ok(employee);
        }
        [HttpGet("getAllEmployees")]
        public async Task<ActionResult<List<GetEmployeeDto>>> GetAllEmployees()
        {
            var employees = await _employeeBusinessManager.GetAllEmployeesAysnc();
            return Ok(employees);
        }

        [HttpPost("deleteEmployee/{id}")]
        public async Task<ActionResult<GetEmployeeDto>> DeleteEmployee(string id)
        {
            var employee = await _employeeBusinessManager.DeleteEmployeeAysnc(id);
            return Ok(employee);
        }
    }
}
