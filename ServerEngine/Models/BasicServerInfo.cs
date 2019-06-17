using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServerEngine.Models.Servers;
using ServerEngine.Models.Versions;

namespace ServerEngine.Models
{
    public class BasicServerInfo : BaseNotificationClass
    {
        public const string BasicServerInfoFilename = ".srvnfo.json";
        public const string DefaultVanillaImageDirectory = "pack://application:,,,/TTServerMaker;component/Img/DefaultServerImages/";

        [JsonIgnore]
        public ServerBase ParentServer;

        private string _name;
        private string _serverType;
        private string _serverImagePath;
        private string _serverFolderPath;

        private bool changingServerType = false;


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
        public VersionBase Version { get; set; }


        [JsonIgnore]
        public string ServerFolderPath
        {
            get { return _serverFolderPath; }
            set { _serverFolderPath = value.EndsWith("/") ? value : value + "/"; }
        }

        /// <summary>
        /// Gets the server type string
        /// </summary>
        public string ServerType
        {
            // Returns the parent servers server type, if that is not null
            // or if 'changingServerType' variable is set to true
            get { return (ParentServer != null && !changingServerType) ? 
                GetParentServerType() : _serverType; }
            set { _serverType = value; }
        }

        public string ServerImagePath
        {
            get
            {
                // Checking if the file still exists / it's a resource, and if not, getting a random image resource
                if (ServerBase.DefaultServerImages.All(x => DefaultVanillaImageDirectory + x != _serverImagePath) && !File.Exists(_serverImagePath))
                {
                    _serverImagePath = DefaultVanillaImageDirectory +
                        ServerBase.DefaultServerImages[(new Random()).Next(0, ServerBase.DefaultServerImages.Length)];
                }

                return _serverImagePath;
            }
            set
            {
                
                _serverImagePath = value;
                OnPropertyChanged();
            }
        }

        private string GetParentServerType()
        {
            return ParentServer.GetType().Name;
        } 


        public BasicServerInfo(ServerBase parentServer = null)
        {
            ParentServer = parentServer;
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
        /// Next time the Save() function is called, it will not save the real serverType string, it will save the given string instead
        /// </summary>
        /// <param name="typeString"></param>
        public void ChangeServerTypeForNextSave(string typeString)
        {
            changingServerType = true;
            this.ServerType = typeString;
        }

        /// <summary>
        /// Loads the basic server info from the given folder. It's static, because this will determine what type of server
        /// needs to be created (vanilla/etc...).
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns>New BasicServerInfo object</returns>
        public static BasicServerInfo LoadBasicServerInfo(string folderPath)
        {
            try
            {
                using (StreamReader reader =
                    new StreamReader(AppSettings.EnforceTrailingBackslash(folderPath) + BasicServerInfoFilename))
                {
                    BasicServerInfo basicServerInfo = JsonConvert.DeserializeObject<BasicServerInfo>(reader.ReadToEnd(),
                        new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Objects});
                    basicServerInfo.ServerFolderPath = folderPath;
                    reader.Close();
                    return basicServerInfo;
                }
            }
            catch(Exception ex)
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
                StreamWriter writer = new StreamWriter(AppSettings.EnforceTrailingBackslash(ServerFolderPath) + BasicServerInfoFilename);
                var settings = new JsonSerializerSettings();
                settings.TypeNameHandling = TypeNameHandling.Objects;
                writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented, settings));
                writer.Close();
            }
            catch(Exception ex)
            {
                throw new FileLoadException("Failed to read the basic server info from file. " + ex.Message);
            }
        }
    }
}
