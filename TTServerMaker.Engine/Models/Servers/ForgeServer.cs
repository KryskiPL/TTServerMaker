// <copyright file="ForgeServer.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Servers;

/// <summary>
/// A Forge server.
/// </summary>
public class ForgeServer : ServerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForgeServer"/> class.
    /// </summary>
    /// <param name="basicServerInfo">The basic server info.</param>
    public ForgeServer(BasicInfo basicServerInfo)
        : base(basicServerInfo)
    {
    }

    /// <inheritdoc/>
    public override string ServerTypeStr
    {
        get { return "Forge"; }
    }
}