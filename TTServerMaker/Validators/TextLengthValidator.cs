// <copyright file="TextLengthValidator.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Validators
{
    using System.Globalization;
    using System.Windows.Controls;

    /// <summary>
    /// Validates the length of a string.
    /// </summary>
    public class TextLengthValidator : ValidationRule
    {
        /// <summary>
        /// Gets or sets the minimum string length. Ignored if set to -1.
        /// </summary>
        public int MinLength { get; set; } = -1;

        /// <summary>
        /// Gets or sets the maximum string length. Ignored if set to -1.
        /// </summary>
        public int MaxLength { get; set; } = -1;

        /// <inheritdoc/>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value.ToString().Trim();

            if (this.MinLength > -1 && this.MaxLength > -1)
            {
                if (text.Length < this.MinLength || text.Length > this.MaxLength)
                {
                    return new ValidationResult(false, $"Should be between {this.MinLength} and {this.MaxLength} characters");
                }
            }

            if (this.MinLength > -1 && this.MinLength > text.Length)
            {
                return new ValidationResult(false, $"Should be at least {this.MinLength} characters");
            }

            if (this.MaxLength > -1 && this.MaxLength < text.Length)
            {
                return new ValidationResult(false, $"Should be shorter {this.MinLength} characters");
            }

            return ValidationResult.ValidResult;
        }
    }
}