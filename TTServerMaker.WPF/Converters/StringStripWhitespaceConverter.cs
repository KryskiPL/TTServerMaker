// <copyright file="StringStripWhitespaceConverter.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.Converters;

using System;
using System.Globalization;
using System.Windows.Data;

/// <summary>
/// Converts a string to it's stripped equivalent.
/// </summary>
internal class StringStripWhitespaceConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString();
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString().Trim();
    }
}