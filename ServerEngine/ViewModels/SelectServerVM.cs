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

    public class SelectServerVM
    {
        /// <summary>
        /// Gets or sets the list of the servers.
        /// </summary>
        public ObservableCollection<ServerBase> Servers { get; set; } = new ObservableCollection<ServerBase>();

        public SelectServerVM()
        {
            this.LoadServers();

            API.APIClient.LoadPricing();
        }

        /// <summary>
        /// Loads the servers
        /// </summary>
        private void LoadServers()
        {
            // Getting directories where the server settings file exists
            var serverDirectories = Directory.GetDirectories(AppSettings.GeneralSettings.ServerFoldersPath)
                .Where(x => File.Exists(Path.Combine(x, BasicServerInfo.BasicServerInfoFilename)))
                .ToArray();

            foreach (string dir in serverDirectories)
            {
                try
                {
                    this.Servers.Add(ServerFactory.CreateNewServerInstance(dir));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to load server. " + ex.Message);
                }
            }

            // Storing and ordering the servers
            this.Servers = new ObservableCollection<ServerBase>(
                this.Servers.OrderByDescending(x => x.BasicInfo.DateLastLoaded)
                .ThenBy(x => x.BasicInfo.Name));
        }

        /// <summary>
        /// Creates a new server
        /// </summary>
        /// <param name="serverName">Server name</param>
        /// <param name="typeString"></param>
        public void CreateNewServer(string serverName, string typeString = "VanillaServer")
        {
            ServerBase newServer = ServerFactory.CreateNewServerFromScratch(serverName, typeString);
            Servers.Insert(0, newServer);
        }

        /// <summary>
        /// Deletes a given server from the harddrive.
        /// </summary>
        /// <param name="serverToDelete">The server to delete.</param>
        public void DeleteServer(ServerBase serverToDelete)
        {
            try
            {
                serverToDelete.Delete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Servers.Remove(serverToDelete);
            }
        }
    }
}