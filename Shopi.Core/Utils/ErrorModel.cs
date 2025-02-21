namespace Shopi.Core.Utils;

public class ErrorModel
{
    public string Title { get; set; }
    public int Status { get; set; }
    public List<string> Errors { get; set; } 
}