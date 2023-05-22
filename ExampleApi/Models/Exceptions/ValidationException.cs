using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Models.Exceptions;

[Serializable]
public class ValidationException : Exception
{
    public ExceptionError Error { get; } = new();

    public ValidationException()
        : base($"Validation exception.")
    {
        Error.Code = 400;
        Error.Description = base.Message;
    }

    private ValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        base.GetObjectData(info, context);
    }
}

public class ValidationProblemDetails : ProblemDetails
{
    public ValidationProblemDetails(ValidationException exception, HttpContext httpContext)
    {
        Type = "https://example-api.com/validation-error";
        Title = "ValidationError";
        Status = exception.Error.Code;
        Detail = exception.Error.Description;
        Instance = httpContext.Request.Path.ToString();
    }
}