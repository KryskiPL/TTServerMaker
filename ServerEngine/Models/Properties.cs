using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ServerEngine.Models.Servers;

namespace ServerEngine.Models
{
    public class Properties
    {
        const string FILENAME = "server.properties";

        private Dictionary<string, string> _properties;
        private readonly ServerBase Server;

        public string this[string PropertyName]
        {
            get
            {
                return _properties[PropertyName];
            }
            set
            {
                _properties[PropertyName] = value;
            }
        }

        public Properties(ServerBase Server)
        {
            this.Server = Server;
        }

        /// <summary>
        /// Reads the .properties file
        /// </summary>
        /// <param name="Path"></param>
        public void LoadFromFile()
        {
            _properties = new Dictionary<string, string>();

            using (StreamReader olv = new StreamReader(Server.FolderPath + FILENAME))
            {
                while(!olv.EndOfStream)
                {
                    string[] SplitLine = olv.ReadLine().Split(new char[] { '=' }, 1);

                    if (SplitLine.Length > 1)
                        _properties.Add(SplitLine[0], SplitLine[1]);
                }

                olv.Close();
            }
        }

        /// <summary>
        /// Returns the dictionary containing the properties
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ToDictionary()
        {
            return _properties;
        }
    }
}
