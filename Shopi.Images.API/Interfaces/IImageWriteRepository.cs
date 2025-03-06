using Shopi.Images.API.Models;

namespace Shopi.Images.API.Interfaces;

public interface IImageWriteRepository
{
    Task<Image> CreateImage(Image image);
}