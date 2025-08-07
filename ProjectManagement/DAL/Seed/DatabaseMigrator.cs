using Common.Enums;
using DAL.Context;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DAL.Seed
{
    public class DatabaseMigrator
    {
        public static async Task SeedDatabaseAsync(IServiceProvider appServiceProvider)
        {
            await using var scope = appServiceProvider.CreateAsyncScope();
            var serviceProvider = scope.ServiceProvider;

            var libraryDbContext = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            await SeedAdminAsync(userManager, roleManager);
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
            {
                await roleManager.CreateAsync(new Role { Name = roleName });
            }
        }
    }
}
