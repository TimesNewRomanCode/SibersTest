namespace ProjectManager_1.DTO.Employees;

public class EmployeeDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? MiddleName { get; set; }

    public string Email { get; set; }
}