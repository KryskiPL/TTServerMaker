// <copyright file="GeneralSettings.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Windows;

    /// <summary>
    /// The general settings of the application.
    /// </summary>
    public class GeneralSettings
    {
        /// <summary>
        /// Defines the name of the application settings file.
        /// </summary>
        [JsonIgnore]
        private const string Filename = "app.settings";

        /// <summary>
        /// The name of the default server folder.
        /// </summary>
        [JsonIgnore]
        private const string DefaultServerFolderName = "My Servers";

        /// <summary>
        /// Gets returns the default server folder path (in the user's documents folder).
        /// </summary>
        /// <returns>The path to the default folder.</returns>
        public static string GetDefaultServersPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DefaultServerFolderName) + "\\";

        /// <summary>
        /// Gets or sets the path to the folder containing the servers with a trailing slash.
        /// </summary>
        public string ServerFoldersPath { get; set; }

        /// <summary>
        /// Gets the full path to the settings file.
        /// </summary>
        [JsonIgnore]
        internal static string FileFullPath => Path.Combine(AppSettings.AppdataFolder, Filename);

        /// <summary>
        /// Writes the settings to a file.
        /// </summary>
        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(FileFullPath))
            {
                writer.Write(JsonConvert.SerializeObject(this));
            }
        }
    }
}