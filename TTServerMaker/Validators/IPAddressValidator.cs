using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TTServerMaker.Validators
{
    class IPAddressValidator : ValidationRule
    {
        public bool CanBeEmpty { get; set; } = false;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (string) value;

            if((string.IsNullOrEmpty(input) && CanBeEmpty) || IPAddress.TryParse(input, out IPAddress _))
                return ValidationResult.ValidResult;
            else
                return  new ValidationResult(false, "Invalid IP address");
        }
    }
}
