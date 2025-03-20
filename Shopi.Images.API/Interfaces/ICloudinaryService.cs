using CloudinaryDotNet.Actions;
using Shopi.Images.API.DTOs;

namespace Shopi.Images.API.Interfaces;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadImage(UploadImageDto dto);
    Task<IEnumerable<Uri>> GetImagesByProductId(Guid productId);
    Task DeleteImageByFileNameAndProductId(string fileName, Guid productId);
}