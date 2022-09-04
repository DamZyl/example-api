using ExampleApi.Models.Enums;

namespace ExampleApi.Models;

public class Comment
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Author { get; set; } = string.Empty;
    public CommentType Type { get; set; }
}