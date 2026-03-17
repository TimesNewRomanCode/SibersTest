using System.ComponentModel.DataAnnotations;

namespace ProjectManager_1.DTO.Employees;

public class UpdateEmployeeDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string? MiddleName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}