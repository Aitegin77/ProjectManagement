using DAL.Entities;
using DTO;
using Mapster;

namespace AppProjectManagement.Extensions
{
    public static class MapsterExtensions
    {
        public static void RegisterMapping(this IApplicationBuilder app)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.EmployeeMapping();
            config.EmployeeProjectMapping();
            config.ProjectMapping();
        }

        private static void EmployeeMapping(this TypeAdapterConfig config)
        {

        }

        private static void EmployeeProjectMapping(this TypeAdapterConfig config)
        {

        }

        private static void ProjectMapping(this TypeAdapterConfig config)
        {
            config.ForType<ProjectDto.Create, Project>()
                .Ignore(dest => dest.Documents)
                .Map(dest => dest.Employees,
                     src => src.EmployeeIds
                        .Select(id => new EmployeeProject() { EmployeeId = id })
                        .ToList());

            config.ForType<Project, ProjectDto.Get>()
                .Map(dest => dest.Employees, src => src.Employees.Select(ep => ep.Employee))
                .Map(dest => dest.DocumentNames, src => src.Documents
                    .Select(d => new ProjectDto.Get() { Id = d.Id, Name = d.Name }));
        }
    }
}
