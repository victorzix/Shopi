using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Images.Application.Commands;
using Shopi.Images.Application.Interfaces;
using Shopi.Images.Domain.Interfaces;

namespace Shopi.Images.API.CommandHandlers;

public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
{
    private readonly IImageReadRepository _readRepository;
    private readonly IImageWriteRepository _writeRepository;
    private readonly ICloudinaryService _cloudinary;

    public DeleteImageCommandHandler(IImageReadRepository readRepository, IImageWriteRepository writeRepository, ICloudinaryService cloudinary)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _cloudinary = cloudinary;
    }
    
    public async Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _readRepository.GetImage(request.Id);
        if (image == null)
        {
            throw new CustomApiException("Erro ao deletar imagem", StatusCodes.Status404NotFound, "Id da imagem não encontrado");
        }

        await _cloudinary.DeleteImageByFileNameAndProductId(image.FileName, image.ProductId);
        await _writeRepository.DeleateImage(image.Id);
    }
}