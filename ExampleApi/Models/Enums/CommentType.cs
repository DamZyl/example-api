using System.ComponentModel;

namespace ExampleApi.Models.Enums;

public enum CommentType
{
    [Description("Pozytywny")]
    Positive = 0,
    [Description("Negatywny")]
    Negative
}