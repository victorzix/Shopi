namespace Shopi.Admin.Application.DTOs;

public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}