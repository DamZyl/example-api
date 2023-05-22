using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Models.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public ExceptionError Error { get; } = new();

    public NotFoundException()
        : base($"Not found exception.")
    {
        Error.Code = 404;
        Error.Description = base.Message;
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        base.GetObjectData(info, context);
    }
}

public class NotFoundProblemDetails : ProblemDetails
{
    public NotFoundProblemDetails(NotFoundException exception, HttpContext httpContext)
    {
        Type = "https://example-api.com/not-found-error";
        Title = "NotFoundError";
        Status = exception.Error.Code;
        Detail = exception.Error.Description;
        Instance = httpContext.Request.Path.ToString();
    }
}