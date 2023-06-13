// <copyright file="BoolToVisibilityConverter.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.Converters;

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

/// <summary>
/// Converts a bool value to a visibility value.
/// </summary>
[ValueConversion(typeof(bool), typeof(Visibility))]
public sealed class BoolToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoolToVisibilityConverter"/> class.
    /// </summary>
    public BoolToVisibilityConverter()
    {
        // set defaults
        this.TrueValue = Visibility.Visible;
        this.FalseValue = Visibility.Collapsed;
    }

    /// <summary>
    /// Gets or sets the visibility value of a True value.
    /// </summary>
    public Visibility TrueValue { get; set; }

    /// <summary>
    /// Gets or sets the visibility value of a False value.
    /// </summary>
    public Visibility FalseValue { get; set; }

    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool)
        {
            return null;
        }

        return (bool)value ? this.TrueValue : this.FalseValue;
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (Equals(value, this.TrueValue))
        {
            return true;
        }

        if (Equals(value, this.FalseValue))
        {
            return false;
        }

        return null;
    }
}