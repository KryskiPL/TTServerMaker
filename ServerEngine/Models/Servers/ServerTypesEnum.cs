// <copyright file="ServerTypesEnum.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Servers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The different type of servers the program supports.
    /// </summary>
    public enum ServerType
    {
        /// <summary>
        /// A vanilla official server.
        /// </summary>
        Vanilla,

        /// <summary>
        /// A Forge, moddable server.
        /// </summary>
        Forge,
    }
}
