using Microsoft.AspNetCore.Mvc;
using ProjectManager_1.DTO.Projects;
using ProjectManager_1.Services;

namespace ProjectManager_1.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _service;

    public ProjectsController(ProjectService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects([FromQuery] ProjectFilterDto filter)
    {
        var projects = await _service.GetProjects(filter);
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var project = await _service.GetById(id);

        if (project == null)
            return NotFound();

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectDto dto)
    {
        var project = await _service.Create(dto);
        return Ok(project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectDto dto)
    {
        try
        {
            var result = await _service.Update(id, dto);

            if (!result)
                return NotFound();
            
            var updatedProject = await _service.GetById(id);
            return Ok(updatedProject);
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

    [HttpPost("{projectId}/employees/{employeeId}")]
    public async Task<IActionResult> AddEmployee(Guid projectId, Guid employeeId)
    {
        var result = await _service.AddEmployee(projectId, employeeId);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpDelete("{projectId}/employees/{employeeId}")]
    public async Task<IActionResult> RemoveEmployee(Guid projectId, Guid employeeId)
    {
        var result = await _service.RemoveEmployee(projectId, employeeId);

        if (!result)
            return NotFound();

        return Ok();
    }
}