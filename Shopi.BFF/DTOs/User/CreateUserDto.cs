﻿namespace Shopi.BFF.DTOs.User;

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Document { get; set; }
    public string? ImageUrl { get; set; }
    public string Role { get; set; }
}