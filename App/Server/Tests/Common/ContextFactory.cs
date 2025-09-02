using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Tests.Data;

namespace Tests.Common
{
    public static class ContextFactory
    {
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();

            SeedEmployees(context);
            SeedProjects(context);

            context.SaveChanges();
            return context;
        }

        public static void Destroy(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        public static void SeedEmployees(AppDbContext context) =>
            context.Employees.AddRange(
                new Employee()
                {
                    FirstName = EmployeeTestData.FirstName,
                    LastName = "Employee-1 last name",
                    Mail = "Employee_1@gmail.com"
                },
                new Employee()
                {
                    FirstName = "Employee-2 first name",
                    LastName = EmployeeTestData.LastName,
                    Mail = "Employee_2@gmail.com"
                },
                new Employee()
                {
                    FirstName = "Employee-3 first name",
                    LastName = "Employee-3 last name",
                    Mail = EmployeeTestData.Mail
                },
                new Employee()
                {
                    FirstName = "Employee-4 first name",
                    LastName = "Employee-4 last name",
                    Mail = "Employee_4@gmail.com"
                }
            );

        public static void SeedProjects(AppDbContext context) =>
            context.Projects.AddRange(
                new Project()
                {
                    Name = ProjectTestData.Name,
                    Customer = "Customer-1",
                    Performer = "Perfomer-1",
                    ManagerId = 4,
                    StartDate = ProjectTestData.Date,
                    Priority = 1
                },
                new Project()
                {
                    Name = "Project-2",
                    Customer = ProjectTestData.Customer,
                    Performer = "Performer-2",
                    ManagerId = 3,
                    StartDate = ProjectTestData.Date,
                    Priority = 2
                },
                new Project()
                {
                    Name = "Project-3",
                    Customer = "Customer-3",
                    Performer = ProjectTestData.Performer,
                    ManagerId = 2,
                    StartDate = ProjectTestData.Date,
                    Priority = 3
                },
                new Project()
                {
                    Name = "Project-4",
                    Customer = "Customer-4",
                    Performer = "Performer-4",
                    ManagerId = 1,
                    StartDate = ProjectTestData.Date,
                    Priority = ProjectTestData.Priority
                }
            );
    }
}
