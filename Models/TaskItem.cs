namespace ProjectManager_1.Models;

using System.ComponentModel.DataAnnotations;

public class TaskItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Comment { get; set; }

    public int Priority { get; set; }

    public TaskItemStatus Status { get; set; }
    
    [Required]
    public Guid AuthorId { get; set; }
    public Employee Author { get; set; } = null!;
    
    public Guid? ExecutorId { get; set; }
    public Employee? Executor { get; set; }
    
    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}