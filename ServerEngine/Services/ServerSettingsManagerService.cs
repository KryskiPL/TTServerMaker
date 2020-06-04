// <copyright file="ServerSettingsManagerService.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using TTServerMaker.Engine.Models.Servers;

    /// <summary>
    /// Takes care of managing <see cref="ServerSettings"/> as a json file in the server folder.
    /// </summary>
    public class ServerSettingsManagerService : IServerSettingsManagerService
    {
        /// <summary>
        /// The name of the file the server settings are stored in.
        /// </summary>
        public const string ServerSettingsFilename = ".server-settings.json";

        /// <inheritdoc/>
        public void Save(ServerBase server)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Path.Combine(server.BasicInfo.Folder, ServerSettingsFilename)))
                {
                    writer.Write(JsonConvert.SerializeObject(server.Settings, Formatting.Indented));
                }

                // TODO use this for every file
                File.SetAttributes(Path.Combine(server.BasicInfo.Folder, ServerSettingsFilename), FileAttributes.Hidden | FileAttributes.ReadOnly);
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Failed to read the server settings from file. " + ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public ServerSettings Load(ServerBase server)
        {
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine(server.BasicInfo.Folder, ServerSettingsFilename)))
                {
                    ServerSettings basicServerInfo = JsonConvert.DeserializeObject<ServerSettings>(reader.ReadToEnd());
                    return basicServerInfo;
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Failed to read the basic server info from file. " + ex.Message, ex);
            }
        }
    }
}
