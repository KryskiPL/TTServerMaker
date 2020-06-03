// <copyright file="ServerInfoLoadingService.cs" company="TThread">
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

    /// <summary>
    /// Takes care of loading and managing the basic information of servers from the root server folder.
    /// </summary>
    public class ServerInfoLoadingService : IServerInfoLoadingService
    {
        /// <inheritdoc/>
        public void CreateRootServerFolder(string rootServerFolder)
        {
            // Making sure the given path is relative
            if (!Path.IsPathRooted(rootServerFolder))
            {
                throw new ArgumentException("Invalid folder path");
            }

            if (!Directory.Exists(rootServerFolder))
            {
                Directory.CreateDirectory(rootServerFolder);
            }
        }
    }
}
