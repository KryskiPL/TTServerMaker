// <copyright file="BasicServerInfo.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.ServerEngine.Models
{
    using Newtonsoft.Json;
    using TTServerMaker.ServerEngine.Models.Servers;
    using TTServerMaker.ServerEngine.Models.Versions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The basic information of the server.
    /// </summary>
    public class BasicServerInfo : BaseNotificationClass
    {
        /// <summary>
        /// The name of the file the basic server info is stored in.
        /// </summary>
        public const string BasicServerInfoFilename = ".srvnfo.json";

        /// <summary>
        /// The path of the default images.
        /// </summary>
        public const string DefaultVanillaImageDirectory = "pack://application:,,,/TTServerMaker.WPF;component/Img/DefaultServerImages/";

        [JsonIgnore]
        public ServerBase parentServer;

        private string name;
        private string serverType;
        private string serverImagePath;
        private string serverFolderPath;

        private bool changingServerType = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicServerInfo"/> class.
        /// </summary>
        /// <param name="parentServer"></param>
        public BasicServerInfo(ServerBase parentServer = null)
        {
            this.parentServer = parentServer;
        }

        /// <summary>
        /// Gets or sets the user defined name of the server.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the date and time when the server was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the server was loaded last.
        /// </summary>
        public DateTime DateLastLoaded { get; set; }

        /// <summary>
        /// Gets or sets the date and time the server was last ran.
        /// </summary>
        public DateTime DateLastRun { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the last server backup.
        /// </summary>
        public DateTime DateLastBackup { get; set; }

        /// <summary>
        /// Gets or sets the version of the server.
        /// </summary>
        public ServerVersion Version { get; set; }

        /// <summary>
        /// Gets or sets the folder where the server files are located.
        /// </summary>
        [JsonIgnore]
        public string ServerFolderPath
        {
            get { return this.serverFolderPath; }
            set { this.serverFolderPath = value.EndsWith("/") ? value : value + "/"; }
        }

        /// <summary>
        /// Gets or sets the server type string
        /// </summary>
        public string ServerType
        {
            // Returns the parent servers server type, if that is not null
            // or if 'changingServerType' variable is set to true
            get
            {
                return (this.parentServer != null && !this.changingServerType) ? this.ParentServerType : this.serverType;
            }

            set { this.serverType = value; }
        }

        /// <summary>
        /// Gets or sets the path to the server's preview image.
        /// </summary>
        public string ServerImagePath
        {
            get
            {
                // Checking if the file still exists / it's a resource, and if not, getting a random image resource
                if (ServerBase.DefaultServerImages.All(x => DefaultVanillaImageDirectory + x != this.serverImagePath) && !File.Exists(this.serverImagePath))
                {
                    this.serverImagePath = DefaultVanillaImageDirectory +
                        ServerBase.DefaultServerImages[new Random().Next(0, ServerBase.DefaultServerImages.Length)];
                }

                return this.serverImagePath;
            }
            set
            {
                this.serverImagePath = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the type of the server.
        /// </summary>
        private string ParentServerType => this.parentServer.GetType().Name;

        /// <summary>
        /// Returns the Type of the server.
        /// </summary>
        /// <returns>The type of the server.</returns>
        public Type GetServerTypeClassType()
        {
            return Type.GetType(typeof(ServerBase).Namespace + "." + this.ServerType);
        }

        /// <summary>
        /// Next time the Save() function is called, it will not save the real serverType string, it will save the given string instead
        /// </summary>
        /// <param name="typeString"></param>
        public void ChangeServerTypeForNextSave(string typeString)
        {
            this.changingServerType = true;
            this.ServerType = typeString;
        }

        /// <summary>
        /// Loads the basic server info from the given folder. It's static, because this will determine what type of server
        /// needs to be created (vanilla/etc...).
        /// </summary>
        /// <param name="folderPath">The full path of the server folder.</param>
        /// <returns>New BasicServerInfo object.</returns>
        public static BasicServerInfo LoadBasicServerInfo(string folderPath)
        {
            try
            {
                using (StreamReader reader =
                    new StreamReader(AppSettings.EnforceTrailingBackslash(folderPath) + BasicServerInfoFilename))
                {
                    BasicServerInfo basicServerInfo = JsonConvert.DeserializeObject<BasicServerInfo>(reader.ReadToEnd(),
                        new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
                    basicServerInfo.ServerFolderPath = folderPath;
                    return basicServerInfo;
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Failed to read the basic server info from file. " + ex.Message);
            }
        }

        /// <summary>
        /// Saves the server info to file
        /// </summary>
        public void SaveBasicServerInfo()
        {
            try
            {
                StreamWriter writer = new StreamWriter(AppSettings.EnforceTrailingBackslash(this.ServerFolderPath) + BasicServerInfoFilename);
                var settings = new JsonSerializerSettings();
                settings.TypeNameHandling = TypeNameHandling.Objects;
                writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented, settings));
                writer.Close();
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Failed to read the basic server info from file. " + ex.Message);
            }
        }
    }
}