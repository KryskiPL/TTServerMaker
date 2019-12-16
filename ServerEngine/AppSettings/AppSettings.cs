// <copyright file="AppSettings.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Windows;

    /// <summary>
    /// The application settings.
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// The name of the folder located in the user's appdata folder.
        /// </summary>
        [JsonIgnore]
        private const string AppdataFolderName = "TTServerMaker";

        static AppSettings()
        {
            // Getting the program's storage folder inside the appdata\local\ folder
            AppdataFolder = EnforceTrailingBackslash(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + AppdataFolderName + "\\";

            // Making sure the folder exists
            if (!Directory.Exists(AppdataFolder))
            {
                Directory.CreateDirectory(AppdataFolder);
            }

            // Making sure the file exists If it doesn't, we will assume it's a first start,
            // but NOT generate the default settings file. That is done after the startup setting are filled in by the user
            FirstLaunch = !File.Exists(GeneralSettings.FileFullPath);

            if (!FirstLaunch)
            {
                LoadSettings();
            }

            if (!FirstLaunch && !Directory.Exists(GeneralSettings.ServerFoldersPath))
            {
                MessageBox.Show(
                    "Oh no, the server folder is missing. You will now be walked through the first steps of the program. " +
                                "If you have moved the server folder, please select it again.", "Server folder missing",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                FirstLaunch = true;
            }
        }

        /// <summary>
        /// Gets the full path of the applications appdata folder.
        /// </summary>
        [JsonIgnore]
        public static string AppdataFolder { get; }

        /// <summary>
        /// Gets a value indicating whether this is the first launch of the application.
        /// </summary>
        [JsonIgnore]
        public static bool FirstLaunch { get; private set; } = true;

        /// <summary>
        /// Gets or sets the application's general settings.
        /// </summary>
        public static GeneralSettings GeneralSettings { get; set; }

        /// <summary>
        /// Makes sure that the string passed ends with a slash.
        /// </summary>
        /// <param name="path">The string to add slash to (by reference).</param>
        public static void EnforceTrailingBackslash(ref string path)
        {
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
        }

        /// <summary>
        /// Returns the given path with a trailing backslash.
        /// </summary>
        /// <param name="path">The path with or without a backslash.</param>
        /// <returns>The path with a trailing backslash.</returns>
        public static string EnforceTrailingBackslash(string path)
        {
            return path.EndsWith("\\") ? path : path + "\\";
        }

        /// <summary>
        /// Saves the application settings.
        /// </summary>
        public static void SaveSettings()
        {
            GeneralSettings.Save();
        }

        /// <summary>
        /// Loads the application settings.
        /// </summary>
        public static void LoadSettings()
        {
            if (!File.Exists(GeneralSettings.FileFullPath))
            {
                return;
            }

            using (StreamReader reader = new StreamReader(GeneralSettings.FileFullPath))
            {
                GeneralSettings = JsonConvert.DeserializeObject<GeneralSettings>(reader.ReadToEnd());
            }
        }
    }
}