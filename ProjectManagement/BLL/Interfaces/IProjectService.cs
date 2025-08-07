using DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto.List>> GetByFilterAsync(string? name = null, string? customer = null,
            string? performer = null, DateOnly? startFrom = null, DateOnly? startTo = null, int? priority = null);
        Task<int> CreateAsync(ProjectDto.Create newProject);
        Task<ProjectDto.Get> GetByIdAsync(int id);
        Task UpdateAsync(ProjectDto.Edit modifiedProject);
        Task DeleteByIdAsync(int id);
    }
}
