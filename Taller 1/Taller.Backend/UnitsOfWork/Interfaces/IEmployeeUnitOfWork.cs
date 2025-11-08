using Taller.Shared.DTOs;
using Taller.Shared.Entities;
using Taller.Shared.Responses;

namespace Taller.Backend.UnitsOfWork.Interfaces;

public interface IEmployeeUnitOfWork
{
    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Employee>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Employee>>> GetAsync();

    Task<ActionResponse<IEnumerable<Employee>>> GetByCoincidenceAsync(string coincidence);
}