using BLL.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository ProjectRepository { get; set; }
        private IDocumentService DocumentService { get; set; }

        public ProjectService(IProjectRepository projectRepository, IDocumentService documentService)
        {
            ProjectRepository = projectRepository;
            DocumentService = documentService;
        }

        /// <summary>
        /// Asynchronously retrieves projects that match the specified filter criteria.
        /// </summary>
        /// <param name="name">Optional project name to filter by.</param>
        /// <param name="customer">Optional customer name to filter by.</param>
        /// <param name="performer">Optional performer name to filter by.</param>
        /// <param name="startFrom">Optional start date (inclusive) to filter by.</param>
        /// <param name="startTo">Optional start date (inclusive) to filter by.</param>
        /// <param name="priority">Optional project priority to filter by.</param>
        /// <returns>A list of projects.</returns>
        public async Task<List<ProjectDto.List>> GetByFilterAsync(ProjectDto.Filter filter)
        {
            var projects = await ProjectRepository
                .GetByFilter(filter.Name, filter.Customer, filter.Performer, 
                    filter.StartFrom, filter.StartTo, filter.Priority)
                .ToListAsync();

            return projects.Adapt<List<ProjectDto.List>>();
        }

        /// <summary>
        /// Asynchronously creates a new project with the specified data.
        /// </summary>
        /// <param name="newProject">The data for the project to be created.</param>
        /// <returns>The ID of the created project.</returns>
        public async Task<int> CreateAsync(ProjectDto.Create newProject)
        {
            var project = newProject.Adapt<Project>();

            await ProjectRepository.AddAsync(project);
            await ProjectRepository.SaveChangesAsync();

            await DocumentService.AddAsync(newProject.Documents, project);

            return project.Id;
        }

        /// <summary>
        /// Asynchronously retrieves a project by its ID along with its associated employees.
        /// </summary>
        /// <param name="id">The unique ID of the project.</param>
        public async Task<ProjectDto.Get> GetByIdAsync(int id)
        {
            var project = await ProjectRepository.GetByIdWithIncludesAsync(id);
            Validate(project);

            return project.Adapt<ProjectDto.Get>();
        }

        /// <summary>
        /// Asynchronously updates an existing project and 
        /// its associated employees based on the provided data.
        /// </summary>
        /// <param name="modifiedProject">The updated project data including employee IDs.</param>
        public async Task UpdateAsync(ProjectDto.Edit updatedProject)
        {
            var project = await ProjectRepository.GetByIdWithIncludesAsync(updatedProject.Id);
            Validate(project);

            updatedProject.Adapt(project);

            UpdateEmployees(updatedProject.EmployeeIds, project!.Employees);

            ProjectRepository.Update(project);
            await ProjectRepository.SaveChangesAsync();

            await DocumentService.UpdateAsync(updatedProject.Documents, project);
        }

        /// <summary>
        /// Asynchronously deletes a project by its ID if it exists.
        /// </summary>
        /// <param name="id">The unique ID of the project to delete.</param>
        public async Task DeleteByIdAsync(int id)
        {
            var project = await ProjectRepository.GetByIdWithIncludesAsync(id);
            Validate(project);

            ProjectRepository.Delete(project!);
            await ProjectRepository.SaveChangesAsync();

            DocumentService.Delete(project!.Documents);
        }

        /// <summary>
        /// Updates the employee associations for a project:
        /// removes employees not present in the provided employeeIds list
        /// and adds new ones that are missing.
        /// </summary>
        /// <param name="employeeIds">The list of employee IDs that should be associated with the project.</param>
        /// <param name="employees">The current collection of employee-project links to be updated.</param>
        private static void UpdateEmployees(List<int> employeeIds, ICollection<EmployeeProject> employees)
        {
            var existingIds = employees.Select(ep => ep.EmployeeId).ToHashSet();

            var toRemove = employees.Where(ep => !employeeIds.Contains(ep.EmployeeId)).ToList();

            var toAddIds = employeeIds.Except(existingIds).ToList();

            toRemove.ForEach(ep => employees.Remove(ep));
            toAddIds.ForEach(id => employees.Add(new EmployeeProject() { EmployeeId = id }));
        }

        /// <summary>
        /// Validate that the given project is not null.
        /// </summary>
        /// <param name="project">The project to check for existence.</param>
        /// <exception cref="ArgumentException">Thrown when the project is null.</exception>
        private static void Validate(Project? project)
        {
            if (project == null)
                throw new ArgumentException("The project with this ID does not exist.");
        }
    }
}
