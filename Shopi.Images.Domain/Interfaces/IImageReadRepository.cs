using Shopi.Images.Domain.Entities;
using Shopi.Images.Domain.Queries;

namespace Shopi.Images.Domain.Interfaces;

public interface IImageReadRepository
{
    Task<Image> GetImage(string id);
    Task<IReadOnlyCollection<Image>> ListImages(QueryImages query);
}