// <copyright file="IBasicInfoManagerService.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Services;

using System.Collections.Generic;
using TTServerMaker.Engine.Models.Servers;

/// <summary>
/// The functionality of loading all avaliable servers.
/// </summary>
public interface IBasicInfoManagerService
{
    /// <summary>
    /// Creates the root server folder, where all servers will be stored.
    /// </summary>
    /// <param name="rootServerFolder">The root server folder.</param>
    void CreateRootServerFolder(string rootServerFolder);

    /// <summary>
    /// Loads the list of all servers' basic infos on the user's computer and returns them asynchronously.
    /// </summary>
    /// <returns>Returns the list of the basic server infos.</returns>
    List<BasicInfo> GetServerInfos();

    /// <summary>
    /// Saves the given server info.
    /// </summary>
    /// <param name="info">The server info to save.</param>
    void SaveServerInfo(BasicInfo info);
}
