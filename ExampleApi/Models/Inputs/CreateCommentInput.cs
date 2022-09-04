using ExampleApi.Models.Enums;

namespace ExampleApi.Models.Inputs;

public class CreateCommentInput
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Author { get; set; } = string.Empty;
    public CommentType Type { get; set; }
}