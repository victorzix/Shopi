﻿namespace Shopi.BFF.DTOs.Category;

public class UpdateCategoryDto
{
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }
}