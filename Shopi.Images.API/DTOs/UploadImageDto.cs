namespace Shopi.Images.API.DTOs;

public class UploadImageDto
{
    public Stream FileStream { get; set; }
    public string FileName { get; set; }
    public Guid ProductId { get; set; }
}