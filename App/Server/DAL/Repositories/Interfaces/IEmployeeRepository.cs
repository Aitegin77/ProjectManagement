using DAL.Entities;
using DAL.Repositories.Abstract;
using System.Linq;

namespace DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IQueryable<Employee> GetByFilter(string? filter);
    }
}
