using Microsoft.EntityFrameworkCore;
using ProjectManager_1.Data;
using ProjectManager_1.DTO.Employees;
using ProjectManager_1.Models;

namespace ProjectManager_1.Services;

public class EmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeDto>> GetAll()
    {
        return await _context.Employees
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                MiddleName = e.MiddleName,
                Email = e.Email
            })
            .ToListAsync();
    }

    public async Task<EmployeeDto?> GetById(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return null;

        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            MiddleName = employee.MiddleName,
            Email = employee.Email
        };
    }

    public async Task<EmployeeDto> Create(CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            Email = dto.Email
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            MiddleName = employee.MiddleName,
            Email = employee.Email
        };
    }

    public async Task<bool> Update(Guid id, UpdateEmployeeDto dto)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return false;

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.MiddleName = dto.MiddleName;
        employee.Email = dto.Email;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return true;
    }
}