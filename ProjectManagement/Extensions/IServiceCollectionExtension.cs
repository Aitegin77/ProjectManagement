using AppProjectManagement.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using DAL.Context;
using DAL.Entities.Identity;
using DAL.Repositories;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppProjectManagement.Extensions
{
    internal static class IServiceCollectionExtension
    {
        internal static void RegisterConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("Default")));
        }

        internal static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => CookieOptionsBuilder.Configure(options, configuration));
        }

        internal static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProjectService, ProjectService>();
        }

        internal static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
        }
    }
}
