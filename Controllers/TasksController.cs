using Microsoft.AspNetCore.Mvc;
using ProjectManager_1.DTO.Tasks;
using ProjectManager_1.Models;
using ProjectManager_1.Services;

namespace ProjectManager_1.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskService _service;

    public TasksController(TaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks(
        [FromQuery] TaskItemStatus? status,
        [FromQuery] Guid? executorId,
        [FromQuery] Guid? projectId,
        [FromQuery] string? sortBy)
    {
        var tasks = await _service.GetTasks(status, executorId, projectId, sortBy);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _service.GetById(id);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        try
        {
            var task = await _service.Create(dto);
            return Ok(task);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateTaskDto dto)
    {
        try
        {
            var result = await _service.Update(id, dto);

            if (!result)
                return NotFound();
            
            var updatedTask = await _service.GetById(id);
            return Ok(updatedTask);
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

    [HttpPut("{taskId}/assign/{employeeId}")]
    public async Task<IActionResult> AssignExecutor(Guid taskId, Guid employeeId)
    {
        var result = await _service.AssignExecutor(taskId, employeeId);

        if (!result)
            return NotFound();

        return Ok();
    }
}