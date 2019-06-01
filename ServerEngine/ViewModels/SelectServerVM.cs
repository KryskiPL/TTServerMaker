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
            //ServerFactory.CreateNewServerFromScratch("Szerverem", "VanillaServer");
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

            Servers = new ObservableCollection<ServerBase>(Servers.OrderByDescending((x => x.BasicInfo.DateLastLoaded)).ThenBy(x => x.BasicInfo.Name));
        }

    }
}
