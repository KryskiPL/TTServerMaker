// <copyright file="AllowedCharactersValidator.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Validators
{
    using System.Globalization;
    using System.Linq;
    using System.Windows.Controls;

    /// <summary>
    /// Validates the characters in a string.
    /// </summary>
    internal class AllowedCharactersValidator : ValidationRule
    {
        /// <summary>
        /// Gets or sets the string containing all the permitted characters.
        /// </summary>
        public virtual string AllowedCharacters { get; set; }

        /// <inheritdoc/>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value.ToString();

            foreach (char c in input)
            {
                if (!this.AllowedCharacters.Contains(c))
                {
                    return new ValidationResult(false, "Invalid character: " + c);
                }
            }

            return ValidationResult.ValidResult;
        }
    }
}