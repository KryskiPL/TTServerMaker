using System;

namespace ServerEngine.Exceptions
{
    public class ServerNotLoadedException : Exception
    {
        public ServerNotLoadedException() : base("Unable to access server property before loading up the server properly.") { }
    }
}
