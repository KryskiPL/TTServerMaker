// <copyright file="BaseNotificationClass.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.ServerEngine
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Implements the basic OnPropertyChanged function required for WPF binding.
    /// </summary>
    public class BaseNotificationClass
    {
        /// <summary>
        /// The event which is fired when a property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the property changed event with the given property's name as a parameter.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}