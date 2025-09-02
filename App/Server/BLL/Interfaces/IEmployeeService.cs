using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto.List>> GetByFilterAsync(string? filter);
        Task<int> CreateAsync(EmployeeDto.Create newEmployee);
        Task<EmployeeDto.Get> GetByIdAsync(int id);
        Task UpdateAsync(EmployeeDto.Edit updatedEmployee);
        Task DeleteByIdAsync(int id);
    }
}
