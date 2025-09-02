using BLL.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository EmployeeRepository { get; set; }

        public EmployeeService(IEmployeeRepository employeeRepository) =>
            EmployeeRepository = employeeRepository;

        /// <summary>
        /// Asynchronously retrieves all employees by filter.
        /// </summary>
        /// <returns>A list of employees./</returns>
        public async Task<List<EmployeeDto.List>> GetByFilterAsync(string? filter)
        {
            var projects = await EmployeeRepository
                .GetByFilter(filter)
                .ToListAsync();

            return projects.Adapt<List<EmployeeDto.List>>();
        }

        /// <summary>
        /// Asynchronously creates a new employee with the specified data.
        /// </summary>
        /// <param name="newEmployee">The data for the employee to be created.</param>
        /// <returns>The ID of the created employee.</returns>
        public async Task<int> CreateAsync(EmployeeDto.Create newEmployee)
        {
            var employee = newEmployee.Adapt<Employee>();

            await EmployeeRepository.AddAsync(employee);
            await EmployeeRepository.SaveChangesAsync();

            return employee.Id;
        }

        /// <summary>
        /// Asynchronously retrieves an employee by its ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        public async Task<EmployeeDto.Get> GetByIdAsync(int id)
        {
            var employee = await EmployeeRepository.GetByIdAsync(id);
            EnsureEmployeeExists(employee);

            return employee.Adapt<EmployeeDto.Get>();
        }

        /// <summary>
        /// Asynchronously updates an existing employee.
        /// </summary>
        /// <param name="updatedEmployee">The data for the employee to be updated.</param>
        public async Task UpdateAsync(EmployeeDto.Edit updatedEmployee)
        {
            var employee = await EmployeeRepository.GetByIdAsync(updatedEmployee.Id);
            EnsureEmployeeExists(employee);

            updatedEmployee.Adapt(employee);

            EmployeeRepository.Update(employee!);
            await EmployeeRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously deletes an employee with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        public async Task DeleteByIdAsync(int id)
        {
            var employee = await EmployeeRepository.GetByIdAsync(id);
            EnsureEmployeeExists(employee);

            EmployeeRepository.Delete(employee!);
            await EmployeeRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Ensures that the given employee is not null.
        /// </summary>
        /// <param name="employee">The employee to check for existence.</param>
        /// <exception cref="ArgumentException">Thrown when the employee is null.</exception>
        private static void EnsureEmployeeExists(Employee? employee)
        {
            if (employee == null)
                throw new ArgumentException("The employee with this ID does not exist.");
        }
    }
}
