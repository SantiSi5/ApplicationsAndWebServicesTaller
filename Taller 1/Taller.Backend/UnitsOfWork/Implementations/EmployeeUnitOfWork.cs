using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
using Taller.Backend.Repositories.Interfaces;
using Taller.Backend.UnitsOfWork.Interfaces;
using Taller.Shared.DTOs;
using Taller.Shared.Entities;
using Taller.Shared.Responses;

namespace Taller.Backend.UnitsOfWork.Implementations;

public class EmployeeUnitOfWork : GenericUnitOfWork<Employee>, IEmployeeUnitOfWork
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeUnitOfWork(IGenericRepository<Employee> repository, IEmployeeRepository employeeRepository) : base(repository)
    {
        _employeeRepository = employeeRepository;
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _employeeRepository.GetTotalRecordsAsync(pagination);

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination) => await _employeeRepository.GetAsync(pagination);

    public override async Task<ActionResponse<Employee>> GetAsync(int id) => await _employeeRepository.GetAsync(id);

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync() => await _employeeRepository.GetAsync();

    public async Task<ActionResponse<IEnumerable<Employee>>> GetByCoincidenceAsync(string coincidence)
    {
        return await _employeeRepository.GetByCoincidenceAsync(coincidence);
    }
}