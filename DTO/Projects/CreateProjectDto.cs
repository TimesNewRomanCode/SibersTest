namespace ProjectManager_1.DTO.Projects;

public class CreateProjectDto
{
    public string Name { get; set; }

    public string CustomerCompany { get; set; }

    public string ExecutorCompany { get; set; }

    public Guid ProjectManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Priority { get; set; }
}