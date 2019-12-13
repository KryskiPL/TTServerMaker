// <copyright file="ServerNotLoadedException.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.ServerEngine.Exceptions
{
    using System;

    /// <summary>
    /// An exception indication that the server is not loaded, so the given action is not avaliable.
    /// </summary>
    [Serializable]
    public class ServerNotLoadedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerNotLoadedException"/> class.
        /// </summary>
        public ServerNotLoadedException()
            : base("Unable to access server property before loading up the server properly.")
        {
        }
    }
}