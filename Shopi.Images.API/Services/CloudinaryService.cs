using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using Shopi.Core.Exceptions;
using Shopi.Images.Application.DTOs;
using Shopi.Images.Application.Interfaces;

namespace Shopi.Images.API.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration configuration)
    {
        var url = configuration["Cloudinary:Url"];
        _cloudinary = new Cloudinary(url);
        _cloudinary.Api.Secure = false;
    }

    public async Task<ImageUploadResult> UploadImage(UploadImageDto dto)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(dto.FileName, dto.FileStream),
            UniqueFilename = false,
            PublicId = dto.FileName,
            Folder = $"products/{dto.ProductId}",
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult;
    }

    public async Task<IEnumerable<Uri>> GetImagesByProductId(Guid productId)
    {
        var folderPath = $"products/{productId}/";

        var searchParams = new ListResourcesByPrefixParams()
        {
            Type = "upload",
            ResourceType = ResourceType.Image,
            Prefix = folderPath,
        };

        var results = await _cloudinary.ListResourcesAsync(searchParams);
        return results.Resources.Select(res => res.SecureUrl);
    }

    public async Task DeleteImageByFileNameAndProductId(string fileName, Guid productId)
    {
        var publicId = $"products/{productId}/{fileName}";
        await _cloudinary.DeleteResourcesAsync(new DelResParams
        {
            PublicIds = new List<string> { publicId },
            Type = "upload",
            ResourceType = ResourceType.Image
        });
    }
}