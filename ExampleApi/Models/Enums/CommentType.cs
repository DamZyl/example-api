using System.ComponentModel.DataAnnotations;

namespace ExampleApi.Models.Enums;

public enum CommentType : int
{
    [Display(Name = "Pozytywny")]
    Positive = 0,
    [Display(Name = "Negatywny")]
    Negative
}