// <copyright file="BasicInfo.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Servers;

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using TTServerMaker.Engine.Models.Versions;

/// <summary>
/// Constains all essential information about a server.
/// </summary>
public class BasicInfo : ObservableObject
{
    private ServerType serverType;
    private string name;
    private DateTime dateLastBackup;
    private DateTime dateLastRun;
    private DateTime dateLastLoaded;
    private string serverFolderPath;
    private ServerVersion version;
    private ServerImage image;

    /// <summary>
    /// Gets or sets the user defined name of the server.
    /// </summary>
    public string Name
    {
        get => this.name;
        set => this.SetProperty(ref this.name, value);
    }

    /// <summary>
    /// Gets or sets the date and time when the server was created.
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets the date and time the server was loaded last.
    /// </summary>
    public DateTime DateLastLoaded { get => this.dateLastLoaded; set => this.SetProperty(ref this.dateLastLoaded, value); }

    /// <summary>
    /// Gets or sets the date and time the server was last ran.
    /// </summary>
    public DateTime DateLastRun { get => this.dateLastRun; set => this.SetProperty(ref this.dateLastRun, value); }

    /// <summary>
    /// Gets or sets the date and time of the last server backup.
    /// </summary>
    public DateTime DateLastBackup { get => this.dateLastBackup; set => this.SetProperty(ref this.dateLastBackup, value); }

    /// <summary>
    /// Gets or sets the folder where the server files are located.
    /// </summary>
    [JsonIgnore]
    public string Folder
    {
        get => this.serverFolderPath;
        set => this.SetProperty(ref this.serverFolderPath, value.EndsWith("/") ? value : value + "/");
    }

    /// <summary>
    /// Gets or sets the server type string.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public ServerType ServerType
    {
        get => this.serverType;
        set => this.SetProperty(ref this.serverType, value);
    }

    /// <summary>
    /// Gets or sets the version of the server.
    /// </summary>
    public ServerVersion Version { get => this.version; set => this.SetProperty(ref this.version, value); }

    /// <summary>
    /// Gets or sets the server's preview image.
    /// </summary>
    public ServerImage Image { get => this.image; set => this.SetProperty(ref this.image, value); }
}
