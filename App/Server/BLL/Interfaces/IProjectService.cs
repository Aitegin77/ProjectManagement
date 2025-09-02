using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto.List>> GetByFilterAsync(ProjectDto.Filter filter);
        Task<int> CreateAsync(ProjectDto.Create newProject);
        Task<ProjectDto.Get> GetByIdAsync(int id);
        Task UpdateAsync(ProjectDto.Edit updatedProject);
        Task DeleteByIdAsync(int id);
    }
}
