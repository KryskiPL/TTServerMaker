// <copyright file="VanillaServer.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Servers
{
    /// <summary>
    /// The vanillia minecraft server.
    /// </summary>
    public class VanillaServer : ServerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VanillaServer"/> class.
        /// </summary>
        /// <param name="basicServerInfo">The basic server info.</param>
        public VanillaServer(ServerSettings basicServerInfo)
            : base(basicServerInfo)
        {
        }

        /// <inheritdoc/>
        public override string ServerTypeStr
        {
            get { return "Vanilla"; }
        }
    }
}