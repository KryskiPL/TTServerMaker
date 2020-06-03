// <copyright file="IServerInfoLoadingService.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The functionality of loading all avaliable servers.
    /// </summary>
    public interface IServerInfoLoadingService
    {
        // TODO - a szerverek listájának beolvasását végző interface

        /// <summary>
        /// Creates the root server folder, where all servers will be stored.
        /// </summary>
        /// <param name="rootServerFolder">The root server folder.</param>
        void CreateRootServerFolder(string rootServerFolder);
    }
}
