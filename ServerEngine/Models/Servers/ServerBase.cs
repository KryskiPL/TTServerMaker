// <copyright file="ServerBase.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Servers
{
    using TTServerMaker.Engine.Exceptions;
    using TTServerMaker.Engine.Models.Versions;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public abstract class ServerBase : BaseNotificationClass
    {
        public const string ServerSettingsFilename = ".server-settings.json";

        /// <summary>
        /// The list of the
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

        public string VersionType { get { return typeof(Version).Name; } }
        public ServerVersion Version { get { return BasicInfo.Version; } set { BasicInfo.Version = value; } }

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

        public abstract string ServerTypeStr { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerBase"/> class.
        /// </summary>
        /// <param name="folderPath">The server folder path</param>
        /// <param name="basicServerInfo">The loaded basic server info</param>
        protected ServerBase(ServerSettings basicServerInfo)
        {
            this.BasicInfo = basicServerInfo;

            if (!Directory.Exists(FolderPath))
                throw new ArgumentException("Server folder does not exist");
        }

        /// <summary>
        /// Loads up more information about the server:
        /// Server.properties
        /// </summary>
        public async Task LoadUpAsync()
        {
            this.Properties = new Properties(this);
            this.Properties.LoadFromFile();

            // TODO

            // Updating last loadup time
            this.BasicInfo.DateLastLoaded = DateTime.Now;
            this.BasicInfo.SaveChanges();

            this.FullyLoadedUp = true;
        }
    }
}