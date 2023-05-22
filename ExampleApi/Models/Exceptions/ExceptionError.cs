namespace ExampleApi.Models.Exceptions;

public class ExceptionError
{
    public int Code { get; set; }

    public string Description { get; set; } = string.Empty;
}