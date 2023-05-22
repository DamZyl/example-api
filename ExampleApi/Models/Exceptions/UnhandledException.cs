using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Models.Exceptions;

[Serializable]
public class UnhandledException : Exception
{
    public ExceptionError Error { get; } = new();

    public UnhandledException()
        : base($"Unhandled exception.")
    {
        Error.Code = 403;
        Error.Description = base.Message;
    }

    protected UnhandledException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        base.GetObjectData(info, context);
    }
}

public class UnhandledProblemDetails : ProblemDetails
{
    public UnhandledProblemDetails(UnhandledException exception, HttpContext httpContext)
    {
        Type = "https://example-api.com/unhandled-error";
        Title = "UnhandledError";
        Status = exception.Error.Code;
        Detail = exception.Error.Description;
        Instance = httpContext.Request.Path.ToString();
    }
}