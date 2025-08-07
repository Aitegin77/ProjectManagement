using BLL.Services;
using DAL.Repositories;
using DTO;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Tests.Data;

namespace Tests.Unit
{
    [Collection("Test collection")]
    public class ProjectServiceTests : BaseTest
    {
        private ProjectService Service { get; }

        public ProjectServiceTests()
        {
            var repository = new ProjectRepository(Context);
            Service = new ProjectService(repository);
        }

        [Fact]
        public async Task GetByFilterAsync_FilterByExistingName_ReturnsProjects()
        {
            // Arrange
            // Act
            var project = await Service.GetByFilterAsync(ProjectTestData.Name);

            // Assert
            project.Should().NotBeEmpty();
            project.Should().OnlyContain(p => p.Name == ProjectTestData.Name);
        }

        [Fact]
        public async Task GetByFilterAsync_FilterByDate_ReturnsProjects()
        {
            // Arrange
            // Act
            var project = await Service.GetByFilterAsync(startFrom: ProjectTestData.Date,
                startTo: ProjectTestData.Date);

            // Assert
            project.Count.Should().BeGreaterThanOrEqualTo(4);
            project.Should().OnlyContain(p => p.StartDate == ProjectTestData.Date);
        }

        [Fact]
        public async Task GetByFilterAsync_FilterByNonExistentName_ReturnsEmpty()
        {
            // Arrange
            var invalidName = "12345";

            // Act
            var project = await Service.GetByFilterAsync(invalidName);

            // Assert
            project.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateAsync_ValidProject_ReturnsId()
        {
            // Arrange
            var testProjectName = "Test project";
            var employeeIds = new List<int>() { EmployeeTestData.Id, EmployeeTestData.IdForDelete };

            // Act
            var projectId = await Service.CreateAsync(
                new ProjectDto.Create()
                {
                    Name = testProjectName,
                    Customer = ProjectTestData.Customer,
                    Performer = ProjectTestData.Performer,
                    StartDate = ProjectTestData.Date,
                    Priority = ProjectTestData.Priority,
                    ManagerId = EmployeeTestData.Id,
                    EmployeeIds = employeeIds
                }
            );

            // Assert
            var project = await Context.Projects
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == projectId &&
                    p.Name == testProjectName);

            project.Should().NotBeNull();
            project.Employees.Select(e => e.EmployeeId).Should().Contain(employeeIds);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsProject()
        {
            // Arrange
            // Act
            var project = await Service.GetByIdAsync(ProjectTestData.Id);

            // Assert
            project.Should().NotBeNull();
            project.Id.Should().Be(ProjectTestData.Id);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var invalidId = 99;

            // Act
            Func<Task> act = async () => await Service.GetByIdAsync(invalidId);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UpdateAsync_ValidProject()
        {
            // Arrange
            var testProjectName = "New test project";

            // Act
            await Service.UpdateAsync(
                new ProjectDto.Edit()
                {
                    Id = ProjectTestData.IdForUpdate,
                    Name = testProjectName,
                    Customer = ProjectTestData.Customer,
                    Performer = ProjectTestData.Performer,
                    StartDate = ProjectTestData.Date,
                    Priority = ProjectTestData.Priority,
                    ManagerId = EmployeeTestData.IdForUpdate
                }
            );

            // Assert
            var newProject = await Context.Projects
                .FirstOrDefaultAsync(p => p.Id == ProjectTestData.IdForUpdate &&
                    p.Name == testProjectName &&
                    p.ManagerId == EmployeeTestData.IdForUpdate);

            newProject.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateAsync_InvalidProject_ThrowsArgumentException()
        {
            // Arrange
            var invalidId = 99;
            var testProjectName = "New test project";

            // Act
            Func<Task> act = async () => await Service.UpdateAsync(
                new ProjectDto.Edit()
                {
                    Id = invalidId,
                    Name = testProjectName,
                    Customer = ProjectTestData.Customer,
                    Performer = ProjectTestData.Performer,
                    StartDate = ProjectTestData.Date,
                    Priority = ProjectTestData.Priority,
                    ManagerId = EmployeeTestData.IdForUpdate
                }
            );

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task DeleteByIdAsync_ValidId()
        {
            // Arrange
            var project = await Context.Projects.FindAsync(ProjectTestData.IdForDelete);
            project.Should().NotBeNull();

            // Act
            await Service.DeleteByIdAsync(ProjectTestData.IdForDelete);

            // Assert
            project = await Context.Projects.FindAsync(ProjectTestData.IdForDelete);
            project.Should().BeNull();
        }

        [Fact]
        public async Task DeleteByIdAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var invalidId = 99;
            var project = await Context.Projects
                .FirstOrDefaultAsync(p => p.Id == invalidId);
            project.Should().BeNull();

            // Act
            Func<Task> act = async () => await Service.DeleteByIdAsync(invalidId);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }
    }
}
