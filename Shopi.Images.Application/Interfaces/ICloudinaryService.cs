using CloudinaryDotNet.Actions;
using Shopi.Images.Application.DTOs;

namespace Shopi.Images.Application.Interfaces;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadImage(UploadImageDto dto);
    Task<IEnumerable<Uri>> GetImagesByProductId(Guid productId);
    Task DeleteImageByFileNameAndProductId(string fileName, Guid productId);
}