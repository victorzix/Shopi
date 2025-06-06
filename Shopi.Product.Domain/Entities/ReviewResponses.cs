﻿namespace Shopi.Product.Domain.Entities;

public class ReviewResponses
{
    public Guid Id { get; set; }
    public Guid ReviewId { get; set; }

    public Review Review { get; set; }
    public Guid ParentId { get; set; }

    public string Comment { get; set; }
    public DateTime PostingDate { get; set; }
    public bool Visible { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}