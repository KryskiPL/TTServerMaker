// <copyright file="MainViewModel.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels.Main;
using TTServerMaker.Engine.Models.Servers;

/// <summary>
/// The view model controlling the main server settings window.
/// </summary>
public class MainViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="loadedServer">The currently loaded server.</param>
    public MainViewModel(ServerBase loadedServer)
    {
        this.Server = loadedServer;
    }

    /// <summary>
    /// Gets the currently loaded server.
    /// </summary>
    public ServerBase Server { get; }
}