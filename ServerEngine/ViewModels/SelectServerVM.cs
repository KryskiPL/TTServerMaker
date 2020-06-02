// <copyright file="SelectServerVM.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using TTServerMaker.Engine.Factories;
    using TTServerMaker.Engine.Models;
    using TTServerMaker.Engine.Models.Servers;

    /// <summary>
    /// The view model of selecting a server.
    /// </summary>
    public class SelectServerVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectServerVM"/> class.
        /// </summary>
        public SelectServerVM()
        {
            this.ServerSettingsList = new ObservableCollection<ServerSettings>(GetServers());
        }

        /// <summary>
        /// Gets or sets the list of the servers.
        /// </summary>
        public ObservableCollection<ServerSettings> ServerSettingsList { get; set; } = new ObservableCollection<ServerSettings>();

        /// <summary>
        /// Gets or sets the server that is currently loaded.
        /// </summary>
        public ServerBase LoadedServer { get; set; }

        /// <summary>
        /// Loads the given server.
        /// </summary>
        /// <param name="serverSettings">The server settings containing the info about the server.</param>
        /// <returns>Returns a loaded server.</returns>
        public async Task<ServerBase> LoadSelectedServerAsync(ServerSettings serverSettings)
        {
            ServerBase server = ServerFactory.CreateNewServerInstance(serverSettings);
            await server.LoadUpAsync();

            this.LoadedServer = server;

            return server;
        }

        /// <summary>
        /// Creates a new server.
        /// </summary>
        /// <param name="serverName">The server name.</param>
        /// <param name="typeString">The server type in... string.</param>
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
            catch
            {
                // TODO - error handling
                throw;
            }
            finally
            {
                this.ServerSettingsList.Remove(serverToDelete);
            }
        }

        /// <summary>
        /// Loads the information about the servers.
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
    }
}