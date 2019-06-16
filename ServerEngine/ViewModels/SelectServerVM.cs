using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ServerEngine.Factories;
using ServerEngine.Models;
using ServerEngine.Models.Servers;

namespace ServerEngine.ViewModels
{
    public class SelectServerVM
    {
        public ObservableCollection<ServerBase> Servers { get; set; } = new ObservableCollection<ServerBase>();


        public SelectServerVM()
        {
            LoadServers();
        }

        private void LoadServers()
        {
            // Getting directories where the server settings file exists
            var serverDirectories = Directory.GetDirectories(AppSettings.GeneralSettings.ServerFoldersPath)
                .Where(x => File.Exists(x + "/" + BasicServerInfo.BasicServerInfoFilename)).ToArray();

            foreach (string dir in serverDirectories)
            {
                try
                {
                    Servers.Add(ServerFactory.CreateNewServerInstance(dir));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to load server. " + ex.Message);
                }
            }

            // Storing and ordering the servers
            Servers = new ObservableCollection<ServerBase>(Servers.OrderByDescending((x => x.BasicInfo.DateLastLoaded)).ThenBy(x => x.BasicInfo.Name));
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
                Servers.Remove(serverToDelete);
            }
            
            
        }

    }
}
