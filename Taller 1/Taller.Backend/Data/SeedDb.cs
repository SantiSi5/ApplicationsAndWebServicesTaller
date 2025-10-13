using Microsoft.EntityFrameworkCore;
using Taller.Shared.Entities;

namespace Taller.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckEmployeesFullAsync();
        await CheckEmployeesAsync();
    }

    private async Task CheckEmployeesFullAsync()
    {
        if (!_context.Employees.Any())
        {
            var employeesSQLScript = File.ReadAllText("Data\\Employees.sql");
            await _context.Database.ExecuteSqlRawAsync(employeesSQLScript);
        }
    }

    private async Task CheckEmployeesAsync()
    {
        if (!_context.Employees.Any())
        {
            _context.Employees.AddRange(
                new Employee { FirstName = "Carlos", LastName = "Pérez", IsActive = true, HireDate = new DateTime(2020, 5, 12), Salary = 2500000m },
                new Employee { FirstName = "María", LastName = "González", IsActive = true, HireDate = new DateTime(2019, 8, 3), Salary = 3200000m },
                new Employee { FirstName = "Juan", LastName = "Rodríguez", IsActive = false, HireDate = new DateTime(2018, 11, 20), Salary = 1800000m },
                new Employee { FirstName = "Ana", LastName = "Martínez", IsActive = true, HireDate = new DateTime(2021, 1, 15), Salary = 2800000m },
                new Employee { FirstName = "Luis", LastName = "Ramírez", IsActive = true, HireDate = new DateTime(2022, 6, 10), Salary = 1500000m },
                new Employee { FirstName = "Laura", LastName = "Fernández", IsActive = false, HireDate = new DateTime(2017, 3, 8), Salary = 2100000m },
                new Employee { FirstName = "Andrés", LastName = "Sánchez", IsActive = true, HireDate = new DateTime(2020, 9, 1), Salary = 3500000m },
                new Employee { FirstName = "Paola", LastName = "Torres", IsActive = true, HireDate = new DateTime(2021, 12, 25), Salary = 2700000m },
                new Employee { FirstName = "Felipe", LastName = "Castro", IsActive = false, HireDate = new DateTime(2019, 2, 18), Salary = 1600000m },
                new Employee { FirstName = "Diana", LastName = "Morales", IsActive = true, HireDate = new DateTime(2023, 4, 30), Salary = 4000000m }
            );
            await _context.SaveChangesAsync();
        }
    }
}