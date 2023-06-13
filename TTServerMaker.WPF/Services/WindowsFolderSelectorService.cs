// <copyright file="WindowsFolderSelectorService.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.Services;

using System.Windows.Forms;
using TTServerMaker.Engine.Services;

/// <summary>
/// The implementation of the <see cref="IFolderSelectorService"/> using the default windows form folder select dialog.
/// </summary>
public class WindowsFolderSelectorService : IFolderSelectorService
{
    /// <inheritdoc/>
    public string SelectFolder(string startingFolder = "", string description = "")
    {
        using (FolderBrowserDialog folderBrowserDialog = new ())
        {
            folderBrowserDialog.SelectedPath = startingFolder;
            folderBrowserDialog.Description = description;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }

            return null;
        }
    }
}
