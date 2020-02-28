// <copyright file="BasicServerInfo.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models
{
    using Newtonsoft.Json;
    using TTServerMaker.Engine.Models.Servers;
    using TTServerMaker.Engine.Models.Versions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Converters;

    public enum ServerType { Vanilla, Forge }

    /// <summary>
    /// The basic information of the server.
    /// </summary>
    public class ServerSettings : BaseNotificationClass
    {
        /// <summary>
        /// The name of the file the basic server info is stored in.
        /// </summary>
        public const string ServerSettingsFileName = ".srvnfo.json";

        /// <summary>
        /// The path of the default images.
        /// </summary>
        public const string DefaultVanillaImageDirectory = "pack://application:,,,/TTServerMaker.WPF;component/Img/DefaultServerImages/";

        private string name;
        private ServerType serverType;
        private string serverImagePath;
        private string serverFolderPath;

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
        [JsonConverter(typeof(StringEnumConverter))]
        public ServerType ServerType
        {
            get => this.serverType;

            set
            {
                this.serverType = value;
                OnPropertyChanged();
            }
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
        /// Loads the server settings from the given folder. It's static, because this will determine what type of server
        /// needs to be created (vanilla/etc...).
        /// </summary>
        /// <param name="folderPath">The full path of the server folder.</param>
        /// <returns>New instance of the <see cref="Models.ServerSettings"/> object.</returns>
        internal static ServerSettings LoadServerSettings(string folderPath)
        {
            try
            {
                using (StreamReader reader =
                    new StreamReader(Path.Combine(folderPath, ServerSettingsFileName)))
                {
                    ServerSettings basicServerInfo = JsonConvert.DeserializeObject<ServerSettings>(reader.ReadToEnd(),
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
        public void SaveChanges()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Path.Combine(this.ServerFolderPath, ServerSettingsFileName)))
                {
                    var settings = new JsonSerializerSettings();
                    settings.TypeNameHandling = TypeNameHandling.Objects;
                    writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented, settings));
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Failed to read the basic server info from file. " + ex.Message);
            }
        }

    }
}