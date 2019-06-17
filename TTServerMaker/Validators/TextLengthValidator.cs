using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TTServerMaker.Validators
{
    public class TextLengthValidator : ValidationRule
    {
        public int MinLength { get; set; } = -1;
        public int MaxLength { get; set; } = -1;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Text = value.ToString().Trim();

            if (MinLength > -1 && MaxLength > -1)
            {
                if (Text.Length < MinLength || Text.Length > MaxLength)
                    return new ValidationResult(false, $"Should be between {MinLength} and {MaxLength} characters");
            }

            if (MinLength > -1 && MinLength > Text.Length)
            {
                return new ValidationResult(false, $"Should be at least {MinLength} characters");
            }

            if (MaxLength > -1 && MaxLength < Text.Length)
            {
                return new ValidationResult(false, $"Should be shorter {MinLength} characters");
            }




            return ValidationResult.ValidResult;
        }
    }
}
