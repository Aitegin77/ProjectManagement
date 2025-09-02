using DTO.Abstract;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class ProjectDto
    {
        public abstract class Base
        {
            public string Name { get; set; }
            public string Customer { get; set; }
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
            public List<IFormFile> Documents { get; set; } = new List<IFormFile>();
            public List<int> EmployeeIds { get; set; } = new List<int>();
        }

        public class Get : Base, IBaseDto
        {
            public int Id { get; set; }
            public EmployeeDto.GetFullName Manager { get; set; }
            public List<DocumentDto.Get> DocumentNames { get; set; } = new List<DocumentDto.Get>();
            public List<EmployeeDto.GetFullName> Employees { get; set; } = new List<EmployeeDto.GetFullName>();
        }

        public class Edit : Create, IBaseDto
        {
            public int Id { get; set; }
        }

        public class Filter
        {
            public string? Name { get; set; } = null;
            public string? Customer { get; set; } = null;
            public string? Performer { get; set; } = null;
            public DateOnly? StartFrom { get; set; } = null;
            public DateOnly? StartTo { get; set; } = null;
            public int? Priority { get; set; } = null;
        }
    }
}
