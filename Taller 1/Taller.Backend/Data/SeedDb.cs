using Microsoft.EntityFrameworkCore;
using Taller.Backend.UnitsOfWork.Interfaces;
using Taller.Shared.Entities;
using Taller.Shared.Enums;

namespace Taller.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IUsersUnitOfWork _usersUnitOfWork;

    public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork)
    {
        _context = context;
        _usersUnitOfWork = usersUnitOfWork;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckEmployeesFullAsync();
        await CheckEmployeesAsync();
        await CheckCountriesFullAsync();
        await CheckCountriesAsync();
        await CheckRolesAsync();
        await CheckUserAsync("1010", "Santiago", "Sierra", "santisierra1212@gmail.com", "317 396 4939", "Calle Luna Calle Sol", UserType.Admin);
    }

    private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
    {
        var user = await _usersUnitOfWork.GetUserAsync(email);
        if (user == null)
        {
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document,
                City = _context.Cities.FirstOrDefault(),
                UserType = userType,
            };

            await _usersUnitOfWork.AddUserAsync(user, "123456");
            await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
    }

    private async Task CheckRolesAsync()
    {
        await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
        await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
    }

    private async Task CheckEmployeesFullAsync()
    {
        if (!_context.Employees.Any())
        {
            var employeesSQLScript = File.ReadAllText("Data\\Employees.sql");
            await _context.Database.ExecuteSqlRawAsync(employeesSQLScript);
        }
    }

    private async Task CheckCountriesFullAsync()
    {
        if (!_context.Countries.Any())
        {
            var countriesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.Countries.Any())
        {
            _context.Countries.Add(new Country
            {
                Name = "Colombia",
                States = [
                    new State()
                    {
                        Name = "Antioquia",
                        Cities = [
                            new City() { Name = "Medellín" },
                            new City() { Name = "Itagüí" },
                            new City() { Name = "Envigado" },
                            new City() { Name = "Bello" },
                            new City() { Name = "Rionegro" },
                        ]
                    },
                    new State()
                    {
                        Name = "Bogotá",
                        Cities = [
                            new City() { Name = "Usaquen" },
                            new City() { Name = "Champinero" },
                            new City() { Name = "Santa fe" },
                            new City() { Name = "Useme" },
                            new City() { Name = "Bosa" },
                        ]
                    },
                ]
            });
            _context.Countries.Add(new Country
            {
                Name = "Estados Unidos",
                States = [
                    new State()
                {
                    Name = "Florida",
                    Cities = [
                        new City() { Name = "Orlando" },
                        new City() { Name = "Miami" },
                        new City() { Name = "Tampa" },
                        new City() { Name = "Fort Lauderdale" },
                        new City() { Name = "Key West" },
                    ]
                },
                new State()
                    {
                        Name = "Texas",
                        Cities = [
                            new City() { Name = "Houston" },
                            new City() { Name = "San Antonio" },
                            new City() { Name = "Dallas" },
                            new City() { Name = "Austin" },
                            new City() { Name = "El Paso" },
                        ]
                    },
                ]
            });
        }
        await _context.SaveChangesAsync();
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