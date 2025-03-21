using AutoMapper;
using CloudinaryDotNet.Actions;
using FluentValidation;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Images.Application.Commands;
using Shopi.Images.Application.DTOs;
using Shopi.Images.Application.Interfaces;
using Shopi.Images.Domain.Interfaces;
using Shopi.Images.Domain.Entities;
using Shopi.Images.Application.Validators;

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
        var validator = new UploadImageCommandValidator();
        var validate = await validator.ValidateAsync(request, cancellationToken);

        if (!validate.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validate.Errors.Select(e => e.ErrorMessage));
        }

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