using DAL.Entities;
using DAL.Repositories.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project?> GetByIdWithEmployeesAsync(int id);
        IQueryable<Project> GetByFilter(string? name, string? customer,
            string? performer, DateOnly? startFrom, DateOnly? startTo, int? priority);
    }
}
