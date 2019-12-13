// <copyright file="FileCorruptedException.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.ServerEngine.Exceptions
{
    using System;

    /// <summary>
    /// Represents errors that occur when a file is corrupted.
    /// </summary>
    [Serializable]
    internal class FileCorruptedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileCorruptedException"/> class.
        /// </summary>
        /// <param name="message">The exeption message.</param>
        /// <param name="filename">Tha path of the corrupted file.</param>
        public FileCorruptedException(string message, string filename)
            : base(message)
        {
            this.Filename = filename;
        }

        /// <summary>
        /// Gets the path of the corrupted file.
        /// </summary>
        public string Filename { get; }
    }
}