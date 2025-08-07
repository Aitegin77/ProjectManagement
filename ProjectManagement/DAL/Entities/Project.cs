using DAL.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public string Customer { get; set; }
        public string Performer { get; set; }
        public int ManagerId { get; set; }
        public Employee Manager { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int Priority { get; set; }
        public ICollection<EmployeeProject> Employees { get; set; } = new List<EmployeeProject>();
    }
}
