namespace ProjectManager_1.Models;

using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(100)]
    public string MiddleName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    
    public ICollection<Project> ManagedProjects { get; set; } = new List<Project>();
    
    public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();

    public ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();

}