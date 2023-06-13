// <copyright file="ServerImage.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Servers;

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

/// <summary>
/// The information about the image associated with the server. Not to be confused with the server icon.
/// </summary>
public class ServerImage : ObservableObject
{
    /// <summary>
    /// The path of the default images.
    /// </summary>
    // TODO models shouldn't know where the pictures come from.
    public const string DefaultImageDirectory = "pack://application:,,,/TTServerMaker.WPF;component/Img/DefaultServerImages/";

    /// <summary>
    /// The list of the images the servers can get by default.
    /// </summary>
    public static readonly string[] DefaultServerImages =
        {
            "village.jpg",
            "swamp.jpg",
            "swamp2.jpg",
            "jungle.jpg",
            "oldgardens.jpg",
            "oldhub.jpg",
            "train.jpg",
            "village.jpg",
        };

    private string path;

    /// <summary>
    /// Gets or sets the path of the server image.
    /// </summary>
    public string Path { get => this.path; set => this.SetProperty(ref this.path, value); }

    /// <summary>
    /// Gets a value indicating whether the image exists.
    /// </summary>
    [JsonIgnore]
    public bool Exists => DefaultServerImages.All(x => DefaultImageDirectory + x != this.Path) && !File.Exists(this.Path);

    /// <summary>
    /// Overwrites the current image with a random default image.
    /// </summary>
    public void SwapToRandomDefaultImage()
    {
        this.Path = DefaultImageDirectory + DefaultServerImages[new Random().Next(DefaultServerImages.Length)];
    }
}
