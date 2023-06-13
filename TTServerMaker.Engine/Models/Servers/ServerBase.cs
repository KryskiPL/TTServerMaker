// <copyright file="ServerBase.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Servers;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// The base information about a server.
/// </summary>
public abstract class ServerBase : ObservableObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServerBase"/> class.
    /// </summary>
    /// <param name="basicServerInfo">The loaded basic server info.</param>
    protected ServerBase(BasicInfo basicServerInfo)
    {
        this.BasicInfo = basicServerInfo;

        this.Properties = new Properties(this);
        this.Properties.LoadFromFile();
    }

    /// <summary>
    /// Gets the server properties.
    /// </summary>
    public Properties Properties { get; private set; }

    /// <summary>
    /// Gets the basic information about the server.
    /// </summary>
    public BasicInfo BasicInfo { get; }

    /// <summary>
    /// Gets the server specific settings.
    /// </summary>
    public ServerSettings Settings { get; }

    /// <summary>
    /// Gets the user friendly name of the server type.
    /// </summary>
    public abstract string ServerTypeStr { get; }
}