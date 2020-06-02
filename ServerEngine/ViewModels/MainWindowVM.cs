// <copyright file="MainWindowVM.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TTServerMaker.Engine.Factories;
    using TTServerMaker.Engine.Models;
    using TTServerMaker.Engine.Models.Servers;

    /// <summary>
    /// The view model controlling the main server settings window.
    /// </summary>
    public class MainWindowVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowVM"/> class.
        /// </summary>
        /// <param name="loadedServer">The currently loaded server.</param>
        public MainWindowVM(ServerBase loadedServer)
        {
            this.Server = loadedServer;
        }

        /// <summary>
        /// Gets the currently loaded server.
        /// </summary>
        public ServerBase Server { get; }
    }
}