using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Shopi.Images.API.Commands;
using Shopi.Images.API.Data;
using Shopi.Images.API.DTOs;
using Shopi.Images.API.Models;
using Shopi.Images.API.Services;

namespace Shopi.Images.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ImagesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddImage(IFormFile file, [FromQuery] Guid productId)
    {
        await using var stream = file.OpenReadStream();
        var image = await _mediator.Send(new UploadImageCommand
        {
            FileStream = stream, FileName = file.FileName, ProductId = productId
        });

        return Created(string.Empty, image.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImage(string id)
    {
        await _mediator.Send(new DeleteImageCommand(id));
        return NoContent();
    }

    // [HttpGet("get-images/{productId}")]
    // public async Task<IActionResult> ListProductImages(Guid productId)
    // {
    //     var imagesUrls = await _cloudinary.GetImagesByProductId(productId);
    //     return Ok(imagesUrls);
    // }
    //
    // [HttpDelete("{productId}/{fileName}")]
    // public async Task<IActionResult> Delete(string fileName, Guid productId)
    // {
    //     await _cloudinary.DeleteImageByAssetId(fileName, productId);
    //     return NoContent();
    // }
}