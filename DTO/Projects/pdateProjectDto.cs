using System.ComponentModel.DataAnnotations;

namespace ProjectManager_1.DTO.Projects;

public class UpdateProjectDto
{
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

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Range(1, 10)]
    public int Priority { get; set; }
}