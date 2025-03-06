using MongoDB.Driver;
using Shopi.Images.API.Data;
using Shopi.Images.API.Interfaces;
using Shopi.Images.API.Models;

namespace Shopi.Images.API.Repositories;

public class ImageWriteRepository : IImageWriteRepository
{
    private readonly IMongoCollection<Image> _images;

    public ImageWriteRepository(MongoDbService dbService)
    {
        _images = dbService.Database?.GetCollection<Image>("images");
    }

    public async Task<Image> CreateImage(Image image)
    {
        await _images.InsertOneAsync(image);
        return image;
    }
}