// <copyright file="IFolderSelectorService.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The functionality of selecting a folder.
/// </summary>
public interface IFolderSelectorService
{
    /// <summary>
    /// Allows the user to select a folder.
    /// </summary>
    /// <param name="startingFolder">The folder the user will start off in.</param>
    /// <param name="description">The description of the folder selector. Shown to the user.</param>
    /// <returns>Returns a folder path or null.</returns>
    string SelectFolder(string startingFolder = "", string description = "");
}
