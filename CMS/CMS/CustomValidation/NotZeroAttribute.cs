using System.ComponentModel.DataAnnotations;

namespace CMS.CustomValidation
{
    public class NotZeroAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return (int)value != 0;
        }
    }
}
