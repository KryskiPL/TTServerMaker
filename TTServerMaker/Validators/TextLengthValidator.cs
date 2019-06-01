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
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Text = (string)value;

            if (Text.Length < MinLength || Text.Length > MaxLength)
                return new ValidationResult(false, $"Text length should be between {MinLength} and {MaxLength}");

            return ValidationResult.ValidResult;
        }
    }
}
