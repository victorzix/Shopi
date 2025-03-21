using MongoDB.Driver;
using Shopi.Images.Infrastructure.Data;
using Shopi.Images.Domain.Interfaces;
using Shopi.Images.Domain.Entities;

namespace Shopi.Images.Infrastructure.Repositories;

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

    public async Task DeleateImage(string id)
    {
        await _images.DeleteOneAsync(Builders<Image>.Filter.Eq(i => i.Id, id));
    }
}