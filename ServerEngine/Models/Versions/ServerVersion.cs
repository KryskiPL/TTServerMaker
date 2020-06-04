// <copyright file="ServerVersion.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Versions
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// A server version.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ServerVersion : IComparable<ServerVersion> // TODO interface
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerVersion"/> class.
        /// </summary>
        /// <param name="versionString">The string describing the version.</param>
        public ServerVersion(string versionString)
        {
            this.VersionString = versionString;
        }

        /// <summary>
        /// Gets or sets the string representing the version. Should be enough to replicate the same version.
        /// </summary>
        [JsonProperty]
        public string VersionString { get; set; }

        /// <summary>
        /// Compares two versions.
        /// </summary>
        /// <param name="b">The left side of the equasion.</param>
        /// <param name="c">The right side of the equasion.</param>
        /// <returns>Returns the obvious result.</returns>
        public static bool operator <(ServerVersion b, ServerVersion c) => b.CompareTo(c) == -1;

        /// <summary>
        /// Compares two versions.
        /// </summary>
        /// <param name="b">The left side of the equasion.</param>
        /// <param name="c">The right side of the equasion.</param>
        /// <returns>Returns the obvious result.</returns>
        public static bool operator >(ServerVersion b, ServerVersion c) => b.CompareTo(c) == 1;

        /// <inheritdoc/>
        public abstract int CompareTo(ServerVersion other);

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.VersionString;
        }
    }
}