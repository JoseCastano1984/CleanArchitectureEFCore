using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmployeeRepository (AppDbContext dbContext) : IEmployeeRepository
{
    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        return await dbContext.Employees.ToListAsync();
    }

    public async Task<Employee> GetEmployeeByIdAsync(Guid id)
    {
        return await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        await dbContext.Employees.AddAsync(employee);
        await dbContext.SaveChangesAsync();
        return employee;
    }
}
