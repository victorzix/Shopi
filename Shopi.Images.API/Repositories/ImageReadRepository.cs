using MongoDB.Driver;
using Shopi.Images.API.Data;
using Shopi.Images.API.Interfaces;
using Shopi.Images.API.Models;

namespace Shopi.Images.API.Repositories;

public class ImageReadRepository : IImageReadRepository
{
    private readonly IMongoCollection<Image> _images;

    public ImageReadRepository(MongoDbService dbService)
    {
        _images = dbService.Database?.GetCollection<Image>("images");
    }

    public async Task<Image> GetImage(string id)
    {
        var filter = Builders<Image>.Filter.Eq(i => i.Id, id);
        return await _images.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<Image>> ListImages(Guid productId)
    {
        var filter = Builders<Image>.Filter.Eq(i => i.ProductId, productId);
        return await _images.Find(filter).ToListAsync();
    }
}