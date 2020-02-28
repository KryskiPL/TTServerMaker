// <copyright file="SelectServerVM.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels
{
    using TTServerMaker.Engine.Factories;
    using TTServerMaker.Engine.Models;
    using TTServerMaker.Engine.Models.Servers;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Collections.Generic;

    public class SelectServerVM
    {
        /// <summary>
        /// Gets or sets the list of the servers.
        /// </summary>
        public ObservableCollection<ServerSettings> ServerSettingsList { get; set; } = new ObservableCollection<ServerSettings>();

        public ServerBase LoadedServer { get; set; }

        public SelectServerVM()
        {
            this.ServerSettingsList = new ObservableCollection<ServerSettings>(GetServers());
        }

        public async Task<ServerBase> LoadSelectedServerAsync(ServerSettings serverSettings)
        {
            ServerBase server = ServerFactory.CreateNewServerInstance(serverSettings);
            await server.LoadUpAsync();

            LoadedServer = server;

            return server;
        }


        /// <summary>
        /// Loads the information about the servers
        /// </summary>
        private static List<ServerSettings> GetServers()
        {
            // Getting directories where the server settings file exists
            var serverDirectories = Directory.GetDirectories(AppSettings.GeneralSettings.ServerFoldersPath)
                .Where(x => File.Exists(Path.Combine(x, TTServerMaker.Engine.Models.ServerSettings.ServerSettingsFileName)))
                .ToArray();

            ObservableCollection<ServerSettings> servers = new ObservableCollection<ServerSettings>();

            foreach (string dir in serverDirectories)
            {
                try
                {
                    servers.Add(ServerSettings.LoadServerSettings(dir));
                }
                catch (Exception ex)
                {
                    // Todo hiba kiírása
                    Console.WriteLine("Failed to load server. " + ex.Message);
                }
            }

            // Storing and ordering the servers
            return servers
                .OrderByDescending(x => x.DateLastLoaded)
                .ThenBy(x => x.Name)
                .ToList();
        }

        /// <summary>
        /// Creates a new server
        /// </summary>
        /// <param name="serverName">Server name</param>
        /// <param name="typeString"></param>
        public void CreateNewServer(string serverName, string typeString = "Vanilla") // TODO ez elég fura
        {
            ServerSettings newServer = ServerFactory.CreateNewServerFolder(serverName, typeString);
            this.ServerSettingsList.Insert(0, newServer);
        }

        /// <summary>
        /// Deletes a given server from the harddrive.
        /// </summary>
        /// <param name="serverToDelete">The server to delete.</param>
        public void DeleteServer(ServerSettings serverToDelete)
        {
            try
            {
                Directory.Delete(serverToDelete.ServerFolderPath, true);
            }
            catch (Exception e)
            {
                // TODO - error handling
                throw;
            }
            finally
            {
                this.ServerSettingsList.Remove(serverToDelete);
            }
        }
    }
}