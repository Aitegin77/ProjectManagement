using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace AppProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService ProjectService { get; }

        public ProjectController(IProjectService projectService) =>
            ProjectService = projectService;

        [HttpGet]
        public async Task<List<ProjectDto.List>> GetByFilterAsync([FromQuery] ProjectDto.Filter filter) =>
            await ProjectService.GetByFilterAsync(filter);

        [HttpPost("Create")]
        public async Task<int> CreateAsync([FromForm] ProjectDto.Create newProject) =>
            await ProjectService.CreateAsync(newProject);

        [HttpGet("{id}")]
        public async Task<ProjectDto.Get> GetByIdAsync(int id) =>
            await ProjectService.GetByIdAsync(id);

        [HttpPut]
        public async Task UpdateAsync([FromBody] ProjectDto.Edit updatedProject) =>
            await ProjectService.UpdateAsync(updatedProject);

        [HttpDelete("{id}")]
        public async Task DeleteByIdAsync(int id) =>
            await ProjectService.DeleteByIdAsync(id);
    }
}
