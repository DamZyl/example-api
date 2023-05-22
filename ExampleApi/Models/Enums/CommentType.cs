using System.ComponentModel;

namespace ExampleApi.Models.Enums;

public enum CommentType
{
    [Description("Positive")]
    Positive = 0,
    [Description("Negative")]
    Negative
}