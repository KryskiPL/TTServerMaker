// <copyright file="IServerSettingsManagerService.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTServerMaker.Engine.Models.Servers;

/// <summary>
/// Takes care of managing the server settings of a server.
/// </summary>
public interface IServerSettingsManagerService
{
    /// <summary>
    /// Saves the server info to file.
    /// </summary>
    /// <param name="server">The server the settings belong to.</param>
    void Save(ServerBase server);

    /// <summary>
    /// Loads the server settings of the given folder and returns it. Does not update it.
    /// </summary>
    /// <param name="server">The server which the settings belong to.</param>
    /// <returns>New instance of the <see cref="ServerSettings"/> object.</returns>
    ServerSettings Load(ServerBase server);
}
