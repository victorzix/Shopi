namespace Shopi.BFF.Models;

public class ErrorModel
{
    public string Title { get; set; }
    public int Status { get; set; }
    public string Errors { get; set; }
}