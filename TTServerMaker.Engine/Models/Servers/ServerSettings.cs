// <copyright file="ServerSettings.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Servers
{
    using GalaSoft.MvvmLight;

    /// <summary>
    /// The basic information of the server.
    /// </summary>
    public class ServerSettings : ObservableObject
    {
        private int ram;

        /// <summary>
        /// Gets or sets the amount of RAM allocated for the server.
        /// </summary>
        public int Ram { get => this.ram; set => this.Set(ref this.ram, value); }
    }
}