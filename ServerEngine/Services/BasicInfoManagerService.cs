// <copyright file="BasicInfoManagerService.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using TTServerMaker.Engine.Models.Servers;

    /// <summary>
    /// Takes care of loading and managing the basic information of servers from the root server folder.
    /// </summary>
    public class BasicInfoManagerService : IBasicInfoManagerService
    {
        /// <summary>
        /// The name of the file the basic server info is stored in.
        /// </summary>
        public const string BasicServerInfoFileName = ".srvnfo.json";

        /// <inheritdoc/>
        public void CreateRootServerFolder(string rootServerFolder)
        {
            // Making sure the given path is relative
            if (!Path.IsPathRooted(rootServerFolder))
            {
                throw new ArgumentException("Invalid folder path");
            }

            if (!Directory.Exists(rootServerFolder))
            {
                Directory.CreateDirectory(rootServerFolder);
            }
        }

        /// <inheritdoc/>
        public List<BasicInfo> GetServerInfos()
        {
            // Getting directories where the basic server infos file exists
            List<string> serverDirectories = Directory.GetDirectories(AppSettings.GeneralSettings.ServerFoldersPath)
                .Where(x => File.Exists(Path.Combine(x, BasicServerInfoFileName)))
                .ToList();

            List<BasicInfo> basicServerInfos = new List<BasicInfo>();

            foreach (string dir in serverDirectories)
            {
                try
                {
                    basicServerInfos.Add(this.LoadInfoFromDirectory(dir));
                }
                catch (Exception ex)
                {
                    // Todo hiba loggolása
                    Console.WriteLine("Failed to load server. " + ex.Message);
                }
            }

            // Storing and ordering the servers
            return basicServerInfos
                .OrderByDescending(x => x.DateLastLoaded)
                .ThenBy(x => x.Name)
                .ToList();
        }

        /// <inheritdoc/>
        public void SaveServerInfo(BasicInfo info)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(info.Folder, BasicServerInfoFileName)))
            {
                writer.Write(JsonConvert.SerializeObject(info));
            }
        }

        private BasicInfo LoadInfoFromDirectory(string directory)
        {
            string fileContent = default;
            BasicInfo basicServerInfo = default;

            // Loading the server info file's content
            using (StreamReader reader = new StreamReader(Path.Combine(directory, BasicServerInfoFileName)))
            {
                fileContent = reader.ReadToEnd();

                // Deserializing the basic server info
                basicServerInfo = JsonConvert.DeserializeObject<BasicInfo>(fileContent);
            }

            return basicServerInfo;
        }
    }
}
