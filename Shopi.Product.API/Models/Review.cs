﻿namespace Shopi.Product.API.Models;

public class Review
{
    public Guid Id { get; set; }
    public Guid AppProductId { get; set; }
    public Guid CustomerId { get; set; }

    public AppProduct AppProduct { get; set; }

    public string? Title { get; set; }
    public string? Comment { get; set; }
    public int? Voting { get; set; } = 0;
    public int Rating { get; set; }
    public DateTime PostingDate { get; set; }
    public bool Visible { get; set; } = true;

    public List<ReviewResponses> Responses { get; set; }
}