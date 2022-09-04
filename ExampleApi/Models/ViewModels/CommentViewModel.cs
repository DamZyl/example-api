namespace ExampleApi.Models.ViewModels;

public class CommentViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}