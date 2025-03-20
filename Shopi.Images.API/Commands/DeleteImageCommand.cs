using MediatR;

namespace Shopi.Images.API.Commands;

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