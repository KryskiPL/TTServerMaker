// <copyright file="ServerBase.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Servers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using TTServerMaker.Engine.Exceptions;
    using TTServerMaker.Engine.Models.Versions;

    /// <summary>
    /// The base information about a server.
    /// </summary>
    public abstract class ServerBase : BaseNotificationClass
    {
        /// <summary>
        /// The name of the file the server settings are stored in.
        /// </summary>
        public const string ServerSettingsFilename = ".server-settings.json";

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

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerBase"/> class.
        /// </summary>
        /// <param name="basicServerInfo">The loaded basic server info.</param>
        protected ServerBase(ServerSettings basicServerInfo)
        {
            this.BasicInfo = basicServerInfo;

            if (!Directory.Exists(this.FolderPath))
            {
                throw new ArgumentException("Server folder does not exist");
            }
        }

        /// <summary>
        /// Gets TODO something to remove.
        /// </summary>
        public string VersionType { get => typeof(Version).Name; }

        /// <summary>
        /// Gets or sets the version of the server.
        /// </summary>
        public ServerVersion Version { get => this.BasicInfo.Version; set => this.BasicInfo.Version = value; }

        /// <summary>
        /// Gets the server properties.
        /// </summary>
        public Properties Properties { get; private set; }

        /// <summary>
        /// Gets the folder the server is located in.
        /// </summary>
        public string FolderPath => this.BasicInfo.ServerFolderPath;

        /// <summary>
        /// Gets or sets a value indicating whether the server has been fully loaded up.
        /// </summary>
        public bool FullyLoadedUp { get; set; }

        /// <summary>
        /// Gets the basic information about the server.
        /// </summary>
        public ServerSettings BasicInfo { get; }

        /// <summary>
        /// Gets TODO somethign to delete.
        /// </summary>
        public abstract string ServerTypeStr { get; }

        /// <summary>
        /// Loads the server info into memory.
        /// </summary>
        /// <returns>Returns the task of loading up.</returns>
        public async Task LoadUpAsync()
        {
            this.Properties = new Properties(this);
            this.Properties.LoadFromFile();

            // TODO

            // Updating last loadup time
            this.BasicInfo.DateLastLoaded = DateTime.Now;
            await this.BasicInfo.SaveChangesAsync();

            this.FullyLoadedUp = true;
        }
    }
}