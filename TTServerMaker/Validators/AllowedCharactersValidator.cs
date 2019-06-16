using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TTServerMaker.Validators
{
    class AllowedCharactersValidator : ValidationRule
    {
        public virtual string AllowedCharacters { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value.ToString();

            foreach (char c in input)
            {
                if(!AllowedCharacters.Contains(c))
                    return new ValidationResult(false, "Invalid character: " + c);
            }

            return ValidationResult.ValidResult;
        }
    }
}
