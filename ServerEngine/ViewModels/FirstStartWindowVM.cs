// <copyright file="FirstStartWindowVM.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using TTServerMaker.Engine;

    /// <summary>
    /// The view model of the initial program setup.
    /// </summary>
    public class FirstStartWindowVM
    {
        /// <summary>
        /// Gets the default server folder path.
        /// </summary>
        public string DefaultServerFolderPath => GeneralSettings.DefaultServersPath;

        /// <summary>
        /// Gets or sets the server folder path the user enters as the preferred folder to store their servers.
        /// </summary>
        public string CustomServerFolderPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the program should use a custom path.
        /// </summary>
        public bool ShouldUseCustom { get; set; }
    }
}
