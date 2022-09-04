using System.ComponentModel.DataAnnotations;

namespace ExampleApi.Extensions;

public static class StringExtensions
{
    public static string ToDisplayName(this Enum value)
    {
        try
        {
            var attributes = (DisplayAttribute[]) value.GetType()
                .GetField(value.ToString())!
                .GetCustomAttributes(typeof(DisplayAttribute), false);

            var output = attributes.Length > 0 ? attributes[0].Name : value.ToString();
            return output ?? string.Empty;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}