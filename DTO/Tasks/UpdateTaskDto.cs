using ProjectManager_1.Models;

namespace ProjectManager_1.DTO.Tasks;

public class UpdateTaskDto
{
    public string Title { get; set; }

    public string? Comment { get; set; }

    public int Priority { get; set; }

    public TaskItemStatus Status { get; set; }

    public Guid? ExecutorId { get; set; }
}