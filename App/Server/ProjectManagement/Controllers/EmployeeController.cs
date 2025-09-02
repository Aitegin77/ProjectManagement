using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace AppProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService EmployeeService { get; }

        public EmployeeController(IEmployeeService employeeService) =>
            EmployeeService = employeeService;

        [HttpGet]
        public async Task<List<EmployeeDto.List>> GetAllAsync([FromQuery] string? filter) =>
            await EmployeeService.GetByFilterAsync(filter);

        [HttpPost("Create")]
        public async Task<int> CreateAsync([FromBody] EmployeeDto.Create newEmployee) =>
            await EmployeeService.CreateAsync(newEmployee);

        [HttpGet("{id}")]
        public async Task<EmployeeDto.Get> GetByIdAsync(int id) =>
            await EmployeeService.GetByIdAsync(id);

        [HttpPut]
        public async Task UpdateAsync([FromBody] EmployeeDto.Edit updatedEmployee) =>
            await EmployeeService.UpdateAsync(updatedEmployee);

        [HttpDelete("{id}")]
        public async Task DeleteByIdAsync(int id) =>
            await EmployeeService.DeleteByIdAsync(id);
    }
}
