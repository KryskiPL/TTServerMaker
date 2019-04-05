using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ServerEngine.Models.Servers;
using System.Reflection;

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

        private string PropertiesFilePath
        {
            get
            {
                return Server.FolderPath + FILENAME;
            }
        }

        public Properties(ServerBase Server)
        {
            this.Server = Server;
        }

        /// <summary>
        /// Reads the default server.properties file (from the resources folder), and sets the properties to default
        /// </summary>
        public async void SetToDefault()
        {
           var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ServerEngine.Resources.defaultServerProperties.txt"; 

            //string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("defaultServerProperties.txt"));

            _properties = new Dictionary<string, string>();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string row = await reader.ReadLineAsync();
                    string[] spl = row.Split('=');

                    if (spl.Length > 1)
                        _properties.Add(spl[0], spl[1]);
                }
                reader.Close();
            }
        }

        /// <summary>
        /// Reads the .properties file
        /// </summary>
        /// <param name="Path"></param>
        public void LoadFromFile()
        {
            // If the file does not yet exist, we load the default values to the properties, and save the file.
            if (!File.Exists(PropertiesFilePath))
            {
                SetToDefault();
                SaveToFile();
                return;
            }

            _properties = new Dictionary<string, string>();

            using (StreamReader olv = new StreamReader(PropertiesFilePath))
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

        public async void SaveToFile()
        {
            StreamWriter writer = new StreamWriter(PropertiesFilePath);

            foreach(KeyValuePair<string, string> property in _properties)
            {
                // Removing invalid characters from the property value
                property.Value.Replace("=", "");

                await writer.WriteLineAsync($"{property.Key}={property.Value}");
            }

            writer.Close();
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
