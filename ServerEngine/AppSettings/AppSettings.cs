using System;
using System.IO;
using Newtonsoft.Json;

namespace ServerEngine
{
    public static class AppSettings
    {
        [JsonIgnore]
        private const string AppdataFolderName = "TTServerMaker";
        
        [JsonIgnore]
        public static string AppdataFolder { get; }

        [JsonIgnore]
        public static bool FirstLaunch { get; set; } = true;

        public static GeneralSettings GeneralSettings { get; set; }

        static AppSettings()
        {
            // Getting the program's storage folder inside the appdata\local\ folder
            AppdataFolder = EnforceTrailingBackslash(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + AppdataFolderName + "\\";

            // Making sure the folder exists
            if (!Directory.Exists(AppdataFolder))
                Directory.CreateDirectory(AppdataFolder);

            // Making sure the file exists If it doesn't, we will assume it's a first start,
            // but NOT generate the default settings file. That is done after the startup setting are filled in by the user
            FirstLaunch = !File.Exists(GeneralSettings.FileFullPath);

            if (!FirstLaunch)
                LoadSettings();
        }

        /// <summary>
        /// Makes sure that the string passed ends with a slash
        /// </summary>
        /// <param name="Path">The string to add slash to (by reference)</param>
        public static void EnforceTrailingBackslash(ref string Path)
        {
            if (!Path.EndsWith("\\"))
                Path += "\\";
        }

        public static string EnforceTrailingBackslash(string Path)
        {
            return (Path.EndsWith("\\")) ? Path : Path + "\\";
        }

        public static void SaveSettings()
        {
            GeneralSettings.Save();
        }

        public static void LoadSettings()
        {
            if (!File.Exists(GeneralSettings.FileFullPath))
                return;


            using (StreamReader reader = new StreamReader(GeneralSettings.FileFullPath))
                GeneralSettings = JsonConvert.DeserializeObject<GeneralSettings>(reader.ReadToEnd());
        }
    }

    public class GeneralSettings
    {
        [JsonIgnore]
        const string Filename = "app.settings";

        [JsonIgnore]
        internal static string FileFullPath { get { return Path.Combine(AppSettings.AppdataFolder, Filename); } }

        [JsonIgnore]
        private const string DefaultServerFolderName = "My Servers";
        private string _serverFoldersPath;

        /// <summary>
        /// The path to the folder containing the servers with a trailing slash
        /// </summary>
        public string ServerFoldersPath { get { return _serverFoldersPath; } set { _serverFoldersPath = AppSettings.EnforceTrailingBackslash(value);  } }

        /// <summary>
        /// Returns the default server folder path (in the user's documents folder)
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultServersPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DefaultServerFolderName) + "\\";
        }



        public void Save()
        {
            StreamWriter writer = new StreamWriter(FileFullPath);

            writer.Write(JsonConvert.SerializeObject(this));

            writer.Close();
        }
    }




}
