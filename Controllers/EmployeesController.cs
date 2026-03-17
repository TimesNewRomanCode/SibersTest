using Microsoft.AspNetCore.Mvc;
using ProjectManager_1.DTO.Employees;
using ProjectManager_1.Services;

namespace ProjectManager_1.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeesController(EmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _service.GetAll();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var employee = await _service.GetById(id);

        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDto dto)
    {
        var employee = await _service.Create(dto);
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateEmployeeDto dto)
    {
        try
        {
            var result = await _service.Update(id, dto);

            if (!result)
                return NotFound();
            
            var updatedEmployee = await _service.GetById(id);
            return Ok(updatedEmployee);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.Delete(id);

        if (!result)
            return NotFound();

        return Ok();
    }
}