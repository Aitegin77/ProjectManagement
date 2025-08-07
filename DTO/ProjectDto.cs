using DTO.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class ProjectDto
    {
        public abstract class Base
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Customer { get; set; }
            [Required]
            public string Performer { get; set; }
            public DateOnly StartDate { get; set; }
            public DateOnly? EndDate { get; set; }
            public int Priority { get; set; }
        }

        public class List : Base, IBaseDto
        {
            public int Id { get; set; }
        }

        public class Create : Base
        {
            public int ManagerId { get; set; }
            public List<int> EmployeeIds { get; set; } = new List<int>();
        }

        public class Get : Base, IBaseDto
        {
            public int Id { get; set; }
            public EmployeeDto.GetFullName Manager { get; set; }
            public List<EmployeeDto.GetFullName> Employees { get; set; }
        }

        public class Edit : Create, IBaseDto
        {
            public int Id { get; set; }
        }
    }
}
