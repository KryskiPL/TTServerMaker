using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using ServerEngine.Exceptions;
using ServerEngine.Models;
using ServerEngine.Models.Servers;

namespace ServerEngine.Factories
{
    public class ServerFactory
    {
        /// <summary>
        /// Creates a new server instance with the appropriate server type by reading the basic server info file
        /// </summary>
        /// <param name="serverPath">The path to the server folder</param>
        /// <returns>New server instance</returns>
        public static ServerBase CreateNewServerInstance(string serverPath)
        {
            BasicServerInfo basicServerInfo = BasicServerInfo.LoadBasicServerInfo(serverPath);

            if (basicServerInfo == null)
                throw new FileCorruptedException("Corrupted server info file", serverPath);

            ServerBase newServer = (Activator.CreateInstance(basicServerInfo.GetServerTypeClassType(), basicServerInfo) as ServerBase);
            return newServer;
        }

        public static ServerBase CreateNewServerFromScratch(string serverName, string typeString)
        {
            #region Generating server folder name from name
            string folderName = serverName;

            // Capitalizing letters after spaces, for better readability
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            folderName = textInfo.ToTitleCase(folderName);

            // Removing forbidden characters from the server name
            List<char> forbiddenCharacters = new List<char>(Path.GetInvalidPathChars());
            forbiddenCharacters.Add(' ');
            forbiddenCharacters.Add('\\');

            foreach (char c in forbiddenCharacters)
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

            #endregion
            
            // Making sure the folder doesn't exist yet
            string newDir = AppSettings.GeneralSettings.ServerFoldersPath + folderName;

            while (Directory.Exists(newDir))
                newDir += "v2";

            newDir = AppSettings.EnforceTrailingBackslash(newDir);

            // Creating directory
            Directory.CreateDirectory(newDir);

            BasicServerInfo basicInfo = new BasicServerInfo
            {
                ServerFolderPath = newDir,
                Name = serverName,
                DateCreated = DateTime.Now,
                DateLastLoaded = DateTime.Now
            };

            basicInfo.ServerImagePath = basicInfo.ServerImagePath;

            // Saving the new basic server info to file
            basicInfo.ChangeServerTypeForNextSave(typeString);
            basicInfo.SaveBasicServerInfo();

            // Loading a new server instance (loading the file written in the last step)
            return CreateNewServerInstance(newDir);
        }
    }
}
