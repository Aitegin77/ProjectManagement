using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
