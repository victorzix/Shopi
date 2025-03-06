using AutoMapper;
using CloudinaryDotNet.Actions;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Images.API.Commands;
using Shopi.Images.API.DTOs;
using Shopi.Images.API.Interfaces;
using Shopi.Images.API.Models;

namespace Shopi.Images.API.CommandHandlers;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, ApiResponses<Image>>
{
    private readonly ICloudinaryService _cloudinary;
    private readonly IImageWriteRepository _repository;
    private readonly IMapper _mapper;

    public UploadImageCommandHandler(ICloudinaryService cloudinary, IMapper mapper, IImageWriteRepository repository)
    {
        _cloudinary = cloudinary;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ApiResponses<Image>> Handle(UploadImageCommand request,
        CancellationToken cancellationToken)
    {
        var cloudinaryImage = await _cloudinary.UploadImage(_mapper.Map<UploadImageDto>(request));
        if (cloudinaryImage.Error != null)
        {
            throw new CustomApiException("Erro ao adicionar imagem", StatusCodes.Status400BadRequest,
                cloudinaryImage.Error.Message);
        }

        var image = await _repository.CreateImage(new Image
        {
            Url = cloudinaryImage.SecureUrl.ToString(),
            FileName = cloudinaryImage.DisplayName,
            ProductId = request.ProductId,
        });

        if (image == null)
        {
            throw new CustomApiException("Erro interno do sistema", StatusCodes.Status500InternalServerError,
                "Não foi possível adicionar a imagem");
        }

        return new ApiResponses<Image>
        {
            Data = image,
            Success = true
        };
    }
}