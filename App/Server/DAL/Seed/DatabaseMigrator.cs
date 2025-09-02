using Common.Enums;
using DAL.Context;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Seed
{
    public class DatabaseMigrator
    {
        public static async Task SeedDatabaseAsync(IServiceProvider appServiceProvider)
        {
            await using var scope = appServiceProvider.CreateAsyncScope();
            var serviceProvider = scope.ServiceProvider;

            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            await SeedAdminAsync(userManager, roleManager);
            await SeedProjectAsync(context);
        }

        private static async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
            {
                string roleName = role.ToString();

                await SeedRoleAsync(roleManager, roleName);

                if (!await userManager.Users.AnyAsync(a => a.UserName == roleName))
                {
                    var user = new User()
                    {
                        UserName = roleName,
                    };
                    await userManager.CreateAsync(user, "123454321");
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        private static async Task SeedRoleAsync(RoleManager<Role> roleManager, string roleName)
        {
            if (!await roleManager.Roles.AnyAsync(r => r.Name == roleName))
                await roleManager.CreateAsync(new Role { Name = roleName });
        }

        private static async Task SeedProjectAsync(AppDbContext context)
        {
            if (await context.Projects.AnyAsync()) return;

            var employees = await SeedEmployeeAsync(context, 4);

            var projects = new Project[]
            {
                new Project()
                {
                    Name = "Project 1",
                    Customer = "Customer 1",
                    Performer = "Performer 1",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(3)),
                    Priority = 1,
                    ManagerId = employees[0].Id,
                    Employees = new List<EmployeeProject>()
                    {
                        new EmployeeProject() { EmployeeId = employees[1].Id },
                        new EmployeeProject() { EmployeeId = employees[2].Id }
                    }
                },
                new Project()
                {
                    Name = "Project 2",
                    Customer = "Customer 2",
                    Performer = "Performer 2",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(4)),
                    Priority = 2,
                    ManagerId = employees[1].Id,
                    Employees = new List<EmployeeProject>()
                    {
                        new EmployeeProject() { EmployeeId = employees[2].Id },
                        new EmployeeProject() { EmployeeId = employees[3].Id }
                    }
                },
                new Project()
                {
                    Name = "Project 3",
                    Customer = "Customer 3",
                    Performer = "Performer 3",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(5)),
                    Priority = 3,
                    ManagerId = employees[2].Id,
                    Employees = new List<EmployeeProject>()
                    {
                        new EmployeeProject() { EmployeeId = employees[3].Id },
                        new EmployeeProject() { EmployeeId = employees[0].Id }
                    }
                },
                new Project()
                {
                    Name = "Project 4",
                    Customer = "Customer 4",
                    Performer = "Performer 4",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(6)),
                    Priority = 4,
                    ManagerId = employees[3].Id,
                    Employees = new List<EmployeeProject>()
                    {
                        new EmployeeProject() { EmployeeId = employees[0].Id },
                        new EmployeeProject() { EmployeeId = employees[1].Id }
                    }
                }
            };

            await context.Projects.AddRangeAsync(projects);
            await context.SaveChangesAsync();
        }

        private static async Task<Employee[]> SeedEmployeeAsync(AppDbContext context, int count)
        {
            var emp = context.Employees;

            if (emp.Count() >= count)
                return emp.Take(count).ToArray();

            var employees = new Employee[]
            {
                new Employee()
                {
                    LastName = "Бошмоюнов",
                    FirstName = "Айтегин",
                    Patronymic = "Алмазович",
                    Mail = "aiteginakmatov@gmail.com"
                },
                new Employee()
                {
                    LastName = "Иванов",
                    FirstName = "Иван",
                    Patronymic = "Иванович",
                    Mail = "ivanov@gmail.com"
                },
                new Employee()
                {
                    LastName = "Сергеев",
                    FirstName = "Сергей",
                    Patronymic = "Сергеевич",
                    Mail = "sergeev@gmail.com"
                },
                new Employee()
                {
                    LastName = "Семенов",
                    FirstName = "Семен",
                    Patronymic = "Семенович",
                    Mail = "semenov@gmail.com"
                }
            };

            await context.Employees.AddRangeAsync(employees);
            await context.SaveChangesAsync();

            return employees;
        }
    }
}
