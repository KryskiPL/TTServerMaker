// <copyright file="ServerEditValidation.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.Validators
{
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;
    using TTServerMaker.Engine.Models.Servers;

    /// <summary>
    /// Validates the settings of a server edit.
    /// </summary>
    internal class ServerEditValidation : ValidationRule
    {
        /// <inheritdoc/>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingGroup bg = value as BindingGroup;

            // Getting the BasicInfo object
            BasicInfo basicInfo = bg.Items[1] as BasicInfo;

            if (basicInfo.Name.Length <= 3)
            {
                return new ValidationResult(false, "The server name must be at least 3 characters long");
            }

            return ValidationResult.ValidResult;
        }
    }
}