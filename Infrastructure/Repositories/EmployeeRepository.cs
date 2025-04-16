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
        employee.Id = Guid.NewGuid();
        await dbContext.Employees.AddAsync(employee);
        await dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> UpdateEmployeeAsync(Guid employeeId, Employee employee)
    {
        var employeeRecord = dbContext.Employees.FirstOrDefault(x => x.Id == employeeId);
        if (employeeRecord is not null)
        {
            employeeRecord.FirstName = employee.FirstName;
            employeeRecord.LastName = employee.LastName;
            employeeRecord.Email = employee.Email;
            employeeRecord.PhoneNumber = employee.PhoneNumber;
            await dbContext.SaveChangesAsync();
            return employeeRecord;
        }

        return employee;    
    }

    public async Task<bool> DeleteEmployeeAsync(Guid employeeId)
    {
        var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
        if (employee is not null)
        {
            dbContext.Employees.Remove(employee);
            return await dbContext.SaveChangesAsync() > 0;
        }
        return false;
        
    }
}
