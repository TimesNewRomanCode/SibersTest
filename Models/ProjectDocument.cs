namespace ProjectManager_1.Models;

using System.ComponentModel.DataAnnotations;

public class ProjectDocument
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string FileName { get; set; }

    public string FilePath { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public Guid ProjectId { get; set; }

    public Project Project { get; set; }
}