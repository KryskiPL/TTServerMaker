using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using ServerEngine.Exceptions;
using ServerEngine.Models.Versions;

namespace ServerEngine.Models.Servers
{
    public abstract class ServerBase : BaseNotificationClass
    {
        public const string ServerSettingsFilename = ".server-settings.json";

        public static readonly string[] DefaultServerImages = 
            { "village.jpg",
                "swamp.jpg",
                "swamp2.jpg",
                "jungle.jpg",
                "oldgardens.jpg",
                "oldhub.jpg",
                "train.jpg",
                "village.jpg" };

        #region Private Properties
        private Properties _properties;
        #endregion

        #region Properties
        public string VersionType { get { return typeof(Version).Name; } }
        public VersionBase Version { get { return BasicInfo.Version; } set { BasicInfo.Version = value; } }
        public Properties Properties
        {
            get
            {
                if (!FullyLoadedUp && !IsLoadingUp)
                    throw new ServerNotLoadedException();

                return _properties;
            }
            private set
            {
                _properties = value;
            }
        }
        public string FolderPath
        {
            get
            {
                return BasicInfo.ServerFolderPath;
            }
            set
            {
                BasicInfo.ServerFolderPath = value.EndsWith("/") ? value : value + "/";
            }
        }
        public bool FullyLoadedUp { get; set; }
        public BasicServerInfo BasicInfo { get; set; }
        public bool IsLoadingUp { get; set; }
        #endregion

        public abstract string ServerTypeStr { get; }

        /// <summary>
        /// Load a server
        /// </summary>
        /// <param name="folderPath">The server folder path</param>
        /// <param name="basicServerInfo">The loaded basic server info</param>
        protected ServerBase(BasicServerInfo basicServerInfo)
        {
            BasicInfo = basicServerInfo;

            if (!Directory.Exists(FolderPath))
                throw new ArgumentException("Server folder does not exist");
            
            BasicInfo.ParentServer = this;
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

            IsLoadingUp = true;

            Properties = new Properties(this);
            Properties.LoadFromFile();

            // TODO

            // Updating last loadup time
            BasicInfo.DateLastLoaded = DateTime.Now;
            BasicInfo.SaveBasicServerInfo();


            IsLoadingUp = false;
            FullyLoadedUp = true;
        }

        
    }
}
