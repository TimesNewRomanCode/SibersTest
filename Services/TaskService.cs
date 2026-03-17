using Microsoft.EntityFrameworkCore;
using ProjectManager_1.Data;
using ProjectManager_1.DTO.Tasks;
using ProjectManager_1.Models;

namespace ProjectManager_1.Services;

public class TaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<TaskDto>> GetTasks(
        TaskItemStatus? status,
        Guid? executorId,
        Guid? projectId,
        string? sortBy)
    {
        var query = _context.Tasks.AsQueryable();

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (executorId.HasValue)
            query = query.Where(t => t.ExecutorId == executorId);

        if (projectId.HasValue)
            query = query.Where(t => t.ProjectId == projectId);

        query = sortBy switch
        {
            "priority" => query.OrderBy(t => t.Priority),
            "title" => query.OrderBy(t => t.Title),
            "status" => query.OrderBy(t => t.Status),
            _ => query.OrderBy(t => t.Title)
        };

        return await query
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Comment = t.Comment,
                Priority = t.Priority,
                Status = t.Status,
                AuthorId = t.AuthorId,
                ExecutorId = t.ExecutorId,
                ProjectId = t.ProjectId
            })
            .ToListAsync();
    }
    
    public async Task<TaskDto?> GetById(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
            return null;

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Comment = task.Comment,
            Priority = task.Priority,
            Status = task.Status,
            AuthorId = task.AuthorId,
            ExecutorId = task.ExecutorId,
            ProjectId = task.ProjectId
        };
    }
    
    public async Task<TaskDto> Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Comment = dto.Comment,
            Priority = dto.Priority,
            Status = dto.Status,
            AuthorId = dto.AuthorId,
            ExecutorId = dto.ExecutorId,
            ProjectId = dto.ProjectId
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Comment = task.Comment,
            Priority = task.Priority,
            Status = task.Status,
            AuthorId = task.AuthorId,
            ExecutorId = task.ExecutorId,
            ProjectId = task.ProjectId
        };
    }
    
    public async Task<bool> Update(Guid id, UpdateTaskDto dto)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
            return false;

        task.Title = dto.Title;
        task.Comment = dto.Comment;
        task.Priority = dto.Priority;
        task.Status = dto.Status;
        task.ExecutorId = dto.ExecutorId;

        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> Delete(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> AssignExecutor(Guid taskId, Guid employeeId)
    {
        var task = await _context.Tasks.FindAsync(taskId);

        if (task == null)
            return false;

        task.ExecutorId = employeeId;

        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<List<TaskDto>> GetTasksByProject(Guid projectId)
    {
        return await _context.Tasks
            .Where(t => t.ProjectId == projectId)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Comment = t.Comment,
                Priority = t.Priority,
                Status = t.Status,
                AuthorId = t.AuthorId,
                ExecutorId = t.ExecutorId,
                ProjectId = t.ProjectId
            })
            .ToListAsync();
    }
}