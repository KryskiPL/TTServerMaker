// <copyright file="RunProcessCommand.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.Commands;

using System;
using System.Diagnostics;
using System.Windows.Input;

/// <summary>
/// Runs the Process suplied as the parameter.
/// </summary>
internal class RunProcessCommand : ICommand
{
    /// <inheritdoc/>
    public event EventHandler CanExecuteChanged
    {
        add { } remove { }
    }

    /// <inheritdoc/>
    public bool CanExecute(object parameter)
    {
        return true;
    }

    /// <inheritdoc/>
    public void Execute(object parameter)
    {
        Process.Start(parameter.ToString());
    }
}