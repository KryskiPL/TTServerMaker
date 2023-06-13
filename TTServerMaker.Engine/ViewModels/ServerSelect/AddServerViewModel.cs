// <copyright file="AddServerViewModel.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels.ServerSelect;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// View model for adding servers.
/// </summary>
public class AddServerViewModel : ObservableObject
{
    private string name;

    /// <summary>
    /// Gets or sets the display name of the server.
    /// </summary>
    public string Name
    {
        get { return this.name; }
        set { this.SetProperty(ref this.name, value); }
    }
}
