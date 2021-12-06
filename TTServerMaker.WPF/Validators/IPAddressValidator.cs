// <copyright file="IPAddressValidator.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.Validators
{
    using System.Globalization;
    using System.Net;
    using System.Windows.Controls;

    /// <summary>
    /// Validates an IP address.
    /// </summary>
    internal class IPAddressValidator : ValidationRule
    {
        /// <summary>
        /// Gets or sets a value indicating whether the validation returns with true even when the given ip address is empty.
        /// </summary>
        public bool CanBeEmpty { get; set; } = false;

        /// <inheritdoc/>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (string)value;

            if ((string.IsNullOrEmpty(input) && this.CanBeEmpty) || IPAddress.TryParse(input, out IPAddress _))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Invalid IP address");
            }
        }
    }
}