﻿namespace Shopi.Admin.API.Models;

public class AppAdmin
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}