using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using ServerEngine.Models;
using ServerEngine.Models.Servers;

namespace TTServerMaker.Validators
{
    class ServerEditValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingGroup bg = value as BindingGroup;

            // Getting the BasicServerInfo object
            BasicServerInfo basicInfo = bg.Items[1] as BasicServerInfo;

            if(basicInfo.Name.Length <= 3)
                return new ValidationResult(false, "The server name must be at least 3 characters long");

            return ValidationResult.ValidResult;
        }
    }
}
