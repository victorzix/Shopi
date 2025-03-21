using MongoDB.Driver;
using Shopi.Images.Infrastructure.Data;
using Shopi.Images.Domain.Interfaces;
using Shopi.Images.Domain.Entities;
using Shopi.Images.Domain.Queries;

namespace Shopi.Images.Infrastructure.Repositories;

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

    public async Task<IReadOnlyCollection<Image>> ListImages(QueryImages query)
    {
        var filter = Builders<Image>.Filter.Eq(i => i.ProductId, query.ProductId);
        var images = await _images
            .Find(filter)
            .Skip((query.PageNumber - 1) * query.Limit)
            .Limit(query.Limit)
            .ToListAsync();
        return images;
    }
}