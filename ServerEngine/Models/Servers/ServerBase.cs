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

        private Properties properties;
        private bool isLoadingUp;

        public string VersionType { get { return typeof(Version).Name; } }
        public ServerVersion Version { get { return BasicInfo.Version; } set { BasicInfo.Version = value; } }

        public Properties Properties
        {
            get
            {
                return properties;
            }
            private set
            {
                properties = value;
            }
        }

        /// <summary>
        /// Gets or sets the folder the server is located in.
        /// </summary>
        public string FolderPath
        {
            get
            {
                return this.BasicInfo.ServerFolderPath;
            }

            set
            {
                this.BasicInfo.ServerFolderPath = AppSettings.EnforceTrailingBackslash(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the server has been fully loaded up.
        /// </summary>
        public bool FullyLoadedUp { get; set; }

        /// <summary>
        /// Gets the basic information about the server.
        /// </summary>
        public BasicServerInfo BasicInfo { get; }

        /// <summary>
        /// Gets a value indicating whether server is currently loading up.
        /// </summary>
        public bool IsLoadingUp
        {
            get
            {
                return this.isLoadingUp;
            }

            private set
            {
                this.isLoadingUp = value;
                this.OnPropertyChanged();
            }
        }

        public abstract string ServerTypeStr { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerBase"/> class.
        /// </summary>
        /// <param name="folderPath">The server folder path</param>
        /// <param name="basicServerInfo">The loaded basic server info</param>
        protected ServerBase(BasicServerInfo basicServerInfo)
        {
            this.BasicInfo = basicServerInfo;

            if (!Directory.Exists(FolderPath))
                throw new ArgumentException("Server folder does not exist");

            this.BasicInfo.parentServer = this;
        }

        /// <summary>
        /// Loads up more information about the server:
        /// Server.properties
        /// </summary>
        public void LoadUp()
        {
            // Preventing double load up
            if (FullyLoadedUp)
                return;

            this.IsLoadingUp = true;

            this.Properties = new Properties(this);
            this.Properties.LoadFromFile();

            // TODO

            // Updating last loadup time
            this.BasicInfo.DateLastLoaded = DateTime.Now;

            this.BasicInfo.SaveBasicServerInfo();

            this.IsLoadingUp = false;
            this.FullyLoadedUp = true;
        }

        /// <summary>
        /// Deletes the server's folder
        /// </summary>
        public void Delete()
        {
            Directory.Delete(this.BasicInfo.ServerFolderPath, true);
        }
    }
}