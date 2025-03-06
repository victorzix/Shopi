using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Shopi.Images.API.Data;
using Shopi.Images.API.DTOs;
using Shopi.Images.API.Models;
using Shopi.Images.API.Services;

namespace Shopi.Images.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IMongoCollection<Image> _images;
    private readonly CloudinaryService _cloudinary;

    public ImagesController(MongoDbService mongoDbService, CloudinaryService cloudinary)
    {
        if (mongoDbService.Database == null)
            throw new Exception("Banco de dados não inicializado!");
        _cloudinary = cloudinary;
        _images = mongoDbService.Database?.GetCollection<Image>("images");
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddImage(IFormFile file, [FromQuery] Guid productId)
    {
        await using var stream = file.OpenReadStream();
        var imageUrl = await _cloudinary.UploadImage(new UploadImageDto
        {
            FileStream = stream, FileName = file.FileName, ProductId = productId
        });

        return Created(string.Empty, imageUrl);
    }

    [HttpGet("get-images/{productId}")]
    public async Task<IActionResult> ListProductImages(Guid productId)
    {
        var imagesUrls = await _cloudinary.GetImagesByProductId(productId);
        return Ok(imagesUrls);
    }

    [HttpDelete("{productId}/{fileName}")]
    public async Task<IActionResult> Delete(string fileName, Guid productId)
    {
        await _cloudinary.DeleteImageByAssetId(fileName, productId);
        return NoContent();
    }
}