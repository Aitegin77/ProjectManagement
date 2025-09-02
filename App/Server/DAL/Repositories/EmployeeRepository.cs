using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext) { }

        public IQueryable<Employee> GetByFilter(string? filter) =>
            Set.Where(e => string.IsNullOrEmpty(filter) ||
                e.LastName.Contains(filter) ||
                e.FirstName.Contains(filter) ||
                (e.Patronymic != null && e.Patronymic.Contains(filter)) ||
                e.Mail.Contains(filter))
            .AsNoTracking();
    }
}
