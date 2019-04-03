using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerEngine.Exceptions
{
    public class ServerNotLoadedException : Exception
    {
        public ServerNotLoadedException() : base("Unable to access server property before loading up the server properly.") { }
    }
}
