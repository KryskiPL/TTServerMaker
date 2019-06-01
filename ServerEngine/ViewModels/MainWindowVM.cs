using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerEngine.Factories;
using ServerEngine.Models;
using ServerEngine.Models.Servers;

namespace ServerEngine.ViewModels
{
    public class MainWindowVM
    {
        public ServerBase Server { get; }

        public MainWindowVM(ServerBase loadedServer)
        {
            this.Server = loadedServer;
        }
    }
}
