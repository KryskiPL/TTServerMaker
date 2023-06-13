// <copyright file="ErrorViewModel.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// The simple view model to use when a simple error alert should be shown.
/// </summary>
public class ErrorViewModel : ObservableRecipient
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
