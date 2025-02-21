using Microsoft.AspNetCore.Mvc;

namespace Shopi.Core.Exceptions;

public class CustomApiException : Exception
{
    public int StatusCode { get; }
    public object? Errors { get; }

    public CustomApiException(string message, int statusCode = StatusCodes.Status400BadRequest, object? errors = null) 
        : base(message)
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    public ProblemDetails ToProblemDetails()
    {
        return new ProblemDetails
        {
            Title = Message,
            Status = StatusCode,
            Extensions = { { "errors", Errors } } 
        };
    }
}