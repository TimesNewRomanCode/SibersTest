namespace ProjectManager_1.DTO.Projects;

public class ProjectFilterDto
{
    public DateTime? StartDateFrom { get; set; }

    public DateTime? StartDateTo { get; set; }

    public int? Priority { get; set; }

    public string? SortBy { get; set; }
}