using System.ComponentModel.DataAnnotations;

namespace ProjectManager_1.Models;

public class Project
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string CustomerCompany { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string ExecutorCompany { get; set; } = string.Empty;
    
    [Required]
    public Guid ProjectManagerId { get; set; }


    public Employee ProjectManager { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Range(1, 10)]
    public int Priority { get; set; }
    
    public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    public ICollection<ProjectDocument> Documents { get; set; } = new List<ProjectDocument>();
}   