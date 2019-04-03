using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerEngine.Models.Servers;
using System.IO;

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
            string[] ServerDirectories = Directory.GetDirectories(AppSettings.GeneralSettings.ServerFoldersPath)
                .Where(x => File.Exists(x + "/" + Models.BasicServerInfo.BasicServerInfoFilename)).ToArray();

            foreach(string Dir in ServerDirectories)
            {
                Servers.Add(ServerBase.CreateNewServerInstance(Dir));
            }
        }
    }
}
