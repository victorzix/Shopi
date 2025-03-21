using Shopi.Images.Domain.Entities;

namespace Shopi.Images.Domain.Interfaces;

public interface IImageWriteRepository
{
    Task<Image> CreateImage(Image image);
    Task DeleateImage(string id);
}