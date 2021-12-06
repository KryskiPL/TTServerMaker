// <copyright file="ErrorViewModel.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;

    /// <summary>
    /// The simple view model to use when a simple error alert should be shown.
    /// </summary>
    public class ErrorViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorViewModel"/> class.
        /// </summary>
        /// <param name="errorMessage">THe error message.</param>
        public ErrorViewModel(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
