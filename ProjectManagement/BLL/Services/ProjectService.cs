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

        public ProjectService(IProjectRepository projectRepository) =>
            ProjectRepository = projectRepository;

        /// <summary>
        /// Retrieves a list of projects that match the specified filter criteria.
        /// </summary>
        /// <param name="name">Optional project name to filter by.</param>
        /// <param name="customer">Optional customer name to filter by.</param>
        /// <param name="performer">Optional performer name to filter by.</param>
        /// <param name="startFrom">Optional start date (inclusive) to filter by.</param>
        /// <param name="startTo">Optional start date (inclusive) to filter by.</param>
        /// <param name="priority">Optional project priority to filter by.</param>
        /// <returns>A list of projects that match the filter.</returns>
        public async Task<List<ProjectDto.List>> GetByFilterAsync(string? name = null, string? customer = null,
            string? performer = null, DateOnly? startFrom = null, DateOnly? startTo = null, int? priority = null)
        {
            var projects = await ProjectRepository
                .GetByFilter(name, customer, performer, startFrom, startTo, priority)
                .ToListAsync();

            return projects.Adapt<List<ProjectDto.List>>();
        }

        /// <summary>
        /// Creates a new project with the specified data and returns its generated ID.
        /// </summary>
        /// <param name="newProject">The data for the project to be created.</param>
        /// <returns>The ID of the newly created project.</returns>
        public async Task<int> CreateAsync(ProjectDto.Create newProject)
        {
            var project = newProject.Adapt<Project>();

            await ProjectRepository.AddAsync(project);
            await ProjectRepository.SaveChangesAsync();

            return project.Id;
        }

        /// <summary>
        /// Retrieves a project by its ID along with its associated employees.
        /// </summary>
        /// <param name="id">The unique ID of the project.</param>
        public async Task<ProjectDto.Get> GetByIdAsync(int id)
        {
            var project = await ProjectRepository.GetByIdWithEmployeesAsync(id);
            EnsureProjectExists(project);

            return project.Adapt<ProjectDto.Get>();
        }

        /// <summary>
        /// Updates an existing project and its associated employees based on the provided data.
        /// </summary>
        /// <param name="modifiedProject">The updated project data including employee IDs.</param>
        public async Task UpdateAsync(ProjectDto.Edit modifiedProject)
        {
            var project = await ProjectRepository.GetByIdWithEmployeesAsync(modifiedProject.Id);
            EnsureProjectExists(project);

            modifiedProject.Adapt(project);

            UpdateEmployees(modifiedProject.EmployeeIds, project!.Employees);

            ProjectRepository.Update(project);
            await ProjectRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a project by its ID if it exists.
        /// </summary>
        /// <param name="id">The unique ID of the project to delete.</param>
        public async Task DeleteByIdAsync(int id)
        {
            var project = await ProjectRepository.GetByIdAsync(id);
            EnsureProjectExists(project);

            ProjectRepository.Delete(project!);
            await ProjectRepository.SaveChangesAsync();
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
        /// Ensures that the given project is not null.
        /// Throws an exception if the project does not exist.
        /// </summary>
        /// <param name="project">The project to check for existence.</param>
        /// <exception cref="ArgumentException">Thrown when the project is null.</exception>
        private static void EnsureProjectExists(Project? project)
        {
            if (project == null)
                throw new ArgumentException("The project with this ID does not exist.");
        }
    }
}
