using Shopi.Images.API.Models;

namespace Shopi.Images.API.Interfaces;

public interface IImageReadRepository
{
    Task<Image> GetImage(string id);
    Task<List<Image>> ListImages(Guid productId);
}