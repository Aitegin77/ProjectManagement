using DAL.Entities.Abstract;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public string Mail { get; set; }
        public ICollection<EmployeeProject> Projects { get; set; }
    }
}
