using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class EntityConfiguration
    {
        public static void ApplyConfigurations(ModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            builder.Entity<UserRole>(ur =>
            {
                ur.HasOne(ur => ur.User)
                      .WithMany(u => u.Roles)
                      .HasForeignKey(ur => ur.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                ur.HasOne(ur => ur.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(ur => ur.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Employee>().HasIndex(e => e.Mail).IsUnique();

            builder.Entity<EmployeeProject>(ep =>
            {
                ep.HasKey(ep => new { ep.ProjectId, ep.EmployeeId });

                ep.HasOne(ep => ep.Project)
                      .WithMany(p => p.Employees)
                      .HasForeignKey(ep => ep.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);

                ep.HasOne(ep => ep.Employee)
                      .WithMany(e => e.Projects)
                      .HasForeignKey(ep => ep.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Project>(p =>
            {
                p.HasIndex(p => p.Name).IsUnique();

                p.HasOne(p => p.Manager)
                      .WithMany()
                      .HasForeignKey(p => p.ManagerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
