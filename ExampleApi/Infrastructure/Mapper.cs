using ExampleApi.Extensions;
using ExampleApi.Models;
using ExampleApi.Models.Enums;
using ExampleApi.Models.ViewModels;

namespace ExampleApi.Infrastructure;

public static class Mapper
{
    public static CommentViewModel MapCommentToViewModel(Comment comment)
    {
        var commentViewModel = new CommentViewModel
        {
            Id = comment.Id,
            Title = comment.Title,
            Message = comment.Message,
            Author = comment.Author,
            Date = comment.Date,
            Type = comment.Type.DisplayName(),
        };

        return commentViewModel;
    }

    public static EnumViewModel MapEnumToViewModel(CommentType type)
    {
        var enumViewModel = new EnumViewModel
        {
            Key = (int) type,
            Value = type.ToString(),
            EnumTypeName = type.DisplayName(),
        };

        return enumViewModel;
    }
}