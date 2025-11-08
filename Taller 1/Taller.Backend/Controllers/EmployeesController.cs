using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taller.Backend.UnitsOfWork.Interfaces;
using Taller.Shared.DTOs;
using Taller.Shared.Entities;

namespace Taller.Backend.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class EmployeesController : GenericController<Employee>
{
    private readonly IEmployeeUnitOfWork _employeesUnitOfWork;

    public EmployeesController(IGenericUnitOfWork<Employee> unitOfWork, IEmployeeUnitOfWork employeesUnitOfWork) : base(unitOfWork)
    {
        _employeesUnitOfWork = employeesUnitOfWork;
    }

    [HttpGet("totalRecords")]
    public override async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _employeesUnitOfWork.GetTotalRecordsAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }

    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
    {
        var response = await _employeesUnitOfWork.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("search/{coincidence}")]
    public async Task<IActionResult> GetByCoincidence(string coincidence)
    {
        var response = await _employeesUnitOfWork.GetByCoincidenceAsync(coincidence);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var action = await _employeesUnitOfWork.GetAsync(id);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return NotFound();
    }
}