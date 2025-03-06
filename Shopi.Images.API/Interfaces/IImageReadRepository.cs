using Shopi.Images.API.Models;
using Shopi.Images.API.Queries;

namespace Shopi.Images.API.Interfaces;

public interface IImageReadRepository
{
    Task<Image> GetImage(string id);
    Task<IReadOnlyCollection<Image>> ListImages(ListImagesQuery query);
}