using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TTServerMaker.Engine.Services
{
    public class NetworkService
    {
        public bool IsPortOpen(int port) => this.IsPortOpen(this.LocalIp, port);

        public bool IsPortOpen(string ip, int port) => this.IsPortOpen(IPAddress.Parse(ip), port);

        public bool IsPortOpen(IPAddress ip, int port)
        {
            TcpListener listener = new TcpListener(ip, port);

            // TODO
            return false;
        }

        /// <summary>
        /// Gets the computer's local ip, or null.
        /// </summary>
        public IPAddress LocalIp
        {
            get
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());

                return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            }
        }
    }
}
