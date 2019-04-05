using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ServerEngine.Models.Servers
{
    public abstract class ServerBase : BaseNotificationClass
    {
        public const string ServerSettingsFilename = ".server-settings.json";

        public static readonly string[] DeafultServerImages = { "village.jpg", "swamp.jpg", "swamp2.jpg", "jungle.jpg", "oldgardens.jpg", "oldhub.jpg", "train.jpg", "village.jpg" };

        #region Private Properties
        private Properties _properties;
        private string _folderPath;
        #endregion

        #region Properties
        public Properties Properties
        {
            get
            {
                if (!FullyLoadedUp && !IsLoadingUp)
                    throw new Exceptions.ServerNotLoadedException();

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
                return _folderPath;
            }
            set
            {
                _folderPath = value.EndsWith("/") ? value : value + "/";
            }
        }
        
        public bool FullyLoadedUp { get; set; } = false;
        public BasicServerInfo BasicInfo { get; set; }

        public bool IsLoadingUp { get; set; }
        #endregion

        public abstract string ServerTypeStr { get; }

        public ServerBase(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
                throw new ArgumentException("Server folder does not exist");

            this.FolderPath = FolderPath;
        }


        public ServerBase(bool NewServer, string ServerName)
        {
            BasicInfo = new BasicServerInfo(this);
            BasicInfo.Name = ServerName;
            CreateFromScratch();
        }

        protected virtual void CreateFromScratch()
        {
            
            string folderName = BasicInfo.Name;

            // Capitalizing letters after spaces, for better readability
            System.Globalization.TextInfo textInfo = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            folderName = textInfo.ToTitleCase(folderName);

            // Removing forbidden characters from the server name
            List<char> ForbiddenCharacters = new List<char>(Path.GetInvalidPathChars());
            ForbiddenCharacters.Add(' ');
            ForbiddenCharacters.Add('\\');

            foreach (char c in ForbiddenCharacters)
            {
                folderName = folderName.Replace(c.ToString(), "");
            }

            // Credit goes to Julien Roncaglia
            // https://stackoverflow.com/a/5459738/2154120
            folderName = Regex.Replace(folderName, "[éèëêð]", "e");
            folderName = Regex.Replace(folderName, "[ÉÈËÊ]", "E");
            folderName = Regex.Replace(folderName, "[àâä]", "a");
            folderName = Regex.Replace(folderName, "[ÀÁÂÃÄÅ]", "A");
            folderName = Regex.Replace(folderName, "[àáâãäå]", "a");
            folderName = Regex.Replace(folderName, "[ÙÚÛÜ]", "U");
            folderName = Regex.Replace(folderName, "[ùúûüµ]", "u");
            folderName = Regex.Replace(folderName, "[òóôõöø]", "o");
            folderName = Regex.Replace(folderName, "[ÒÓÔÕÖØ]", "O");
            folderName = Regex.Replace(folderName, "[ìíîï]", "i");
            folderName = Regex.Replace(folderName, "[ÌÍÎÏ]", "I");
            folderName = Regex.Replace(folderName, "[š]", "s");
            folderName = Regex.Replace(folderName, "[Š]", "S");
            folderName = Regex.Replace(folderName, "[ñ]", "n");
            folderName = Regex.Replace(folderName, "[Ñ]", "N");
            folderName = Regex.Replace(folderName, "[ç]", "c");
            folderName = Regex.Replace(folderName, "[Ç]", "C");
            folderName = Regex.Replace(folderName, "[ÿ]", "y");
            folderName = Regex.Replace(folderName, "[Ÿ]", "Y");
            folderName = Regex.Replace(folderName, "[ž]", "z");
            folderName = Regex.Replace(folderName, "[Ž]", "Z");
            folderName = Regex.Replace(folderName, "[Ð]", "D");
            folderName = Regex.Replace(folderName, "[œ]", "oe");
            folderName = Regex.Replace(folderName, "[Œ]", "Oe");


            // Making sure the folder doesn't exist yet
            string newDir = AppSettings.GeneralSettings.ServerFoldersPath + folderName;
            
            while (Directory.Exists(newDir))
                newDir += "v2";

            newDir = AppSettings.EnforceTrailingBackslash(newDir);

            // Creating directory
            Directory.CreateDirectory(newDir);

            this.FolderPath = newDir;

            BasicInfo.DateCreated = DateTime.Now;
            BasicInfo.ServerImagePath = BasicInfo.ServerImagePath;
            BasicInfo.SaveBasicServerInfo();

        }

        /// <summary>
        /// Loads up more information about the server:
        /// Server.properties
        /// </summary>
        public void LoadUp()
        {
            // Preventing double loadup
            if (FullyLoadedUp)
                return;

            IsLoadingUp = true;

            Properties = new Properties(this);
            Properties.LoadFromFile();

            // TODO

            // Updating last loadup time
            BasicInfo.DateLastLoaded = DateTime.Now;


            IsLoadingUp = false;
            FullyLoadedUp = true;
        }

        /// <summary>
        /// Creates a new server instance with the appropriate server type by reading the basic server info file
        /// </summary>
        /// <param name="ServerPath">The path to the server folder</param>
        /// <returns>New server instance</returns>
        public static ServerBase CreateNewServerInstance(string ServerPath)
        {
            try
            {
                BasicServerInfo basicServerInfo = BasicServerInfo.LoadBasicServerInfo(ServerPath);
                ServerBase NewServer = (Activator.CreateInstance(basicServerInfo.GetServerTypeClassType(), ServerPath) as ServerBase);
                NewServer.BasicInfo = basicServerInfo;
                return NewServer;
            }
            catch { throw; }
        }
    }
}
