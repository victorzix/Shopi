using MediatR;
using Shopi.Core.Utils;
using Shopi.Images.Domain.Entities;

namespace Shopi.Images.Application.Commands;

public class UploadImageCommand : IRequest<ApiResponses<Image>>
{
    public Stream FileStream { get; set; }
    public string FileName { get; set; }
    public Guid ProductId { get; set; }

    public UploadImageCommand()
    {
    }

    public UploadImageCommand(Stream fileStream, string fileName, Guid productId)
    {
        FileStream = fileStream;
        FileName = fileName;
        ProductId = productId;
    }
}