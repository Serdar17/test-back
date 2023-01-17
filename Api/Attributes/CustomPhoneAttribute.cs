using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Api.Attributes;

public class CustomPhoneAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
            return false;
        var pattern = @"^7\d{10}$";
        if (Regex.IsMatch((string) value, pattern))
            return true;
        return false;
    }
}