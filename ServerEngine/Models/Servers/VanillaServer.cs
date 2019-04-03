using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerEngine.Models.Servers
{
    public class VanillaServer : ServerBase
    {
        public VanillaServer(string FolderPath) : base (FolderPath)
        {

        }

        public VanillaServer(bool NewServer, string ServerName) : base(NewServer, ServerName)
        {

        }

        public override string ServerTypeStr => "Vanilla";
    }
}
