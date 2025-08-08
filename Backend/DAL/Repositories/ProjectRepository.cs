using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Project?> GetByIdWithEmployeesAsync(int id) =>
            await GetWithEmployees().FirstOrDefaultAsync(p => p.Id == id);

        public IQueryable<Project> GetByFilter(string? name, string? customer,
            string? performer, DateOnly? startFrom, DateOnly? startTo, int? priority) =>
            Set.Where(p => string.IsNullOrEmpty(name) || p.Name.Contains(name))
                .Where(p => string.IsNullOrEmpty(customer) || p.Customer.Contains(customer))
                .Where(p => string.IsNullOrEmpty(performer) || p.Performer.Contains(performer))
                .Where(p => !startFrom.HasValue || p.StartDate >= startFrom.Value)
                .Where(p => !startTo.HasValue || p.StartDate <= startTo.Value)
                .Where(p => !priority.HasValue || p.Priority == priority.Value)
                .AsNoTracking();

        private IQueryable<Project> GetWithEmployees() =>
            Set.Include(p => p.Manager)
                .Include(p => p.Employees)
                    .ThenInclude(ep => ep.Employee)
                .AsSplitQuery();
    }
}
