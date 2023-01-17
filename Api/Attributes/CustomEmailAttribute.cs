using System.ComponentModel.DataAnnotations;

namespace Api.Attributes;

public class CustomEmailAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }

        if (!(value is string valueAsString))
        {
            return false;
        }
        
        var index = valueAsString.IndexOf('@');

        return
            index > 0 &&
            index != valueAsString.Length - 1 &&
            index == valueAsString.LastIndexOf('@') && 
            valueAsString.Contains('.');
    }
}