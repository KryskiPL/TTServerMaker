// <copyright file="DialogCloser.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Makes it possible to bind to a window's dialogresult. Source: https://stackoverflow.com/a/3329467/2154120 .
    /// </summary>
    public static class DialogCloser
    {
        /// <summary>
        /// The DialogResult dependency property.
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
                "DialogResult",
                typeof(bool?),
                typeof(DialogCloser),
                new PropertyMetadata(DialogResultChanged));

        /// <summary>
        /// Sets the dialogresult of the given window.
        /// </summary>
        /// <param name="target">The target window.</param>
        /// <param name="value">The value to set to.</param>
        public static void SetDialogResult(Window target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }

        /// <summary>
        /// Called when the dialog result changes.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="e">The arguments.</param>
        private static void DialogResultChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                window.DialogResult = e.NewValue as bool?;
            }
        }
    }
}
