using MediatR;

namespace Shopi.Images.Application.Commands;

public class DeleteImageCommand : IRequest
{
    public string Id { get; set; }

    public DeleteImageCommand()
    {
    }

    public DeleteImageCommand(string id)
    {
        Id = id;
    }
}