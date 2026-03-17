using Microsoft.EntityFrameworkCore;
using ProjectManager_1.Data;
using ProjectManager_1.DTO.Projects;
using ProjectManager_1.Models;

namespace ProjectManager_1.Services;

public class ProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<ProjectDto>> GetProjects(ProjectFilterDto filter)
    {
        var query = _context.Projects.AsQueryable();

        if (filter.StartDateFrom.HasValue)
        {
            query = query.Where(p => p.StartDate >= filter.StartDateFrom.Value);
        }

        if (filter.StartDateTo.HasValue)
        {
            query = query.Where(p => p.StartDate <= filter.StartDateTo.Value);
        }

        if (filter.Priority.HasValue)
        {
            query = query.Where(p => p.Priority == filter.Priority.Value);
        }

        query = filter.SortBy switch
        {
            "priority" => query.OrderBy(p => p.Priority),
            "startDate" => query.OrderBy(p => p.StartDate),
            "name" => query.OrderBy(p => p.Name),
            _ => query.OrderBy(p => p.StartDate)
        };

        return await query
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                CustomerCompany = p.CustomerCompany,
                ExecutorCompany = p.ExecutorCompany,
                ProjectManagerId = p.ProjectManagerId,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Priority = p.Priority
            })
            .ToListAsync();
    }
    
    public async Task<ProjectDto?> GetById(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
            return null;

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            CustomerCompany = project.CustomerCompany,
            ExecutorCompany = project.ExecutorCompany,
            ProjectManagerId = project.ProjectManagerId,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Priority = project.Priority
        };
    }
    
    public async Task<ProjectDto> Create(CreateProjectDto dto)
    {
        var project = new Project
        {
            Name = dto.Name,
            CustomerCompany = dto.CustomerCompany,
            ExecutorCompany = dto.ExecutorCompany,
            ProjectManagerId = dto.ProjectManagerId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Priority = dto.Priority
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            CustomerCompany = project.CustomerCompany,
            ExecutorCompany = project.ExecutorCompany,
            ProjectManagerId = project.ProjectManagerId,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Priority = project.Priority
        };
    }
    
    public async Task<bool> Update(Guid id, UpdateProjectDto dto)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
            return false;

        project.Name = dto.Name;
        project.CustomerCompany = dto.CustomerCompany;
        project.ExecutorCompany = dto.ExecutorCompany;
        project.ProjectManagerId = dto.ProjectManagerId;
        project.StartDate = dto.StartDate;
        project.EndDate = dto.EndDate;
        project.Priority = dto.Priority;

        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> Delete(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
            return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> AddEmployee(Guid projectId, Guid employeeId)
    {
        var exists = await _context.ProjectMembers
            .AnyAsync(pm => pm.ProjectId == projectId && pm.EmployeeId == employeeId);

        if (exists)
            return false;

        var member = new ProjectMember
        {
            ProjectId = projectId,
            EmployeeId = employeeId
        };

        _context.ProjectMembers.Add(member);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> RemoveEmployee(Guid projectId, Guid employeeId)
    {
        var member = await _context.ProjectMembers
            .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.EmployeeId == employeeId);

        if (member == null)
            return false;

        _context.ProjectMembers.Remove(member);
        await _context.SaveChangesAsync();

        return true;
    }
}