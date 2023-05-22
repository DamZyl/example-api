using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Models.Exceptions;

[Serializable]
public class InternalServerException : Exception
{
    public ExceptionError Error { get; } = new();

    public InternalServerException()
        : base($"Server exception.")
    {
        Error.Code = 500;
        Error.Description = base.Message;
    }

    protected InternalServerException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        base.GetObjectData(info, context);
    }
}

public class InternalServerProblemDetails : ProblemDetails
{
    public InternalServerProblemDetails(InternalServerException exception, HttpContext httpContext)
    {
        Type = "https://example-api.com/internal-server-error";
        Title = "InternalServerError";
        Status = exception.Error.Code;
        Detail = exception.Error.Description;
        Instance = httpContext.Request.Path.ToString();
    }
}