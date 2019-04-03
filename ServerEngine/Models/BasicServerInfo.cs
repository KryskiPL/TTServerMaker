using Newtonsoft.Json;
using ServerEngine.Models.Servers;
using System;
using System.IO;

namespace ServerEngine.Models
{
    public class BasicServerInfo : BaseNotificationClass
    {
        public const string BasicServerInfoFilename = ".srvnfo.json";
        readonly private ServerBase ParentServer;

        private string _name;
        private string _serverType;


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateCreated { get; set; }
        public DateTime DateLastLoaded { get; set; }
        public DateTime DateLastRun { get; set; }
        public DateTime DateLastBackup { get; set; }
        public Versions.VersionBase Version { get; set; }
        public string ServerType
        {
            get
            {
                if (ParentServer != null)
                    return ParentServerType;
                else
                    return _serverType;
            }
            set
            {
                _serverType = value;
            }
        }

        private string ParentServerType { get => ParentServer.GetType().Name; }


        public BasicServerInfo(ServerBase parentServer = null)
        {
            this.ParentServer = parentServer;
        }

        /// <summary>
        /// Returns the Type of the server
        /// </summary>
        /// <returns></returns>
        public Type GetServerTypeClassType()
        {
            return Type.GetType(typeof(ServerBase).Namespace + "." + ServerType);
        }

        /// <summary>
        /// Loads the basic server info from the given folder. It's static, because this will determine what type of server
        /// needs to be created (vanilla/etc...).
        /// </summary>
        /// <param name="FolderPath"></param>
        /// <returns>New BasicServerInfo object</returns>
        public static BasicServerInfo LoadBasicServerInfo(string FolderPath)
        {
            try
            {
                BasicServerInfo basicServerInfo;
                StreamReader reader = new StreamReader(AppSettings.EnforceTrailingBackslash(FolderPath) + BasicServerInfoFilename);
                basicServerInfo = JsonConvert.DeserializeObject<BasicServerInfo>(reader.ReadToEnd());
                reader.Close();
                return basicServerInfo;
            }
            catch
            {
                throw new FileLoadException("Failed to read the basic server info from file.");
            }
        }

        public void SaveBasicServerInfo()
        {
            try
            {
                StreamWriter writer = new StreamWriter(AppSettings.EnforceTrailingBackslash(ParentServer.FolderPath) + BasicServerInfoFilename);
                writer.Write(JsonConvert.SerializeObject(this));
                writer.Close();
            }
            catch
            {
                throw new FileLoadException("Failed to read the basic server info from file.");
            }
        }

    }
}
