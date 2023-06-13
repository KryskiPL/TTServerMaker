// <copyright file="FirstStartWindowViewModel.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels.FirstStart;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using TTServerMaker.Engine;
using TTServerMaker.Engine.Services;
using TTServerMaker.Engine.ViewModels;

/// <summary>
/// The view model of the initial program setup.
/// </summary>
public class FirstStartViewModel : ObservableRecipient
{
    private bool shouldUseCustom;
    private string customServerFolderPath;
    private ErrorViewModel errorViewModel;
    private bool? isDone;

    /// <summary>
    /// Initializes a new instance of the <see cref="FirstStartViewModel"/> class.
    /// </summary>
    public FirstStartViewModel()
    {
        // Commands
        this.RootServerFolderConfirmCommand = new RelayCommand(this.ConfirmRootServerFolder, () => !this.ShouldUseCustomPath || !string.IsNullOrEmpty(this.CustomServerFolderPath));
        this.DismissErrorMessageCommand = new RelayCommand(() => this.ErrorViewModel = null);
        this.PromptFolderSelectCommand = new RelayCommand(this.SelectFolder);
    }

    /// <summary>
    /// Gets or sets the command for when the user confirms tha path of the server folder.
    /// </summary>
    public RelayCommand RootServerFolderConfirmCommand { get; set; }

    /// <summary>
    /// Gets or sets the command for dismissing the error message.
    /// </summary>
    public RelayCommand DismissErrorMessageCommand { get; set; }

    /// <summary>
    /// Gets or sets the command for prompting the root server folder selection dialog.
    /// </summary>
    public RelayCommand PromptFolderSelectCommand { get; set; }

    /// <summary>
    /// Gets or sets the view model about any possible errors.
    /// </summary>
    public ErrorViewModel ErrorViewModel { get => this.errorViewModel; set => this.SetProperty(ref this.errorViewModel, value); }

    /// <summary>
    /// Gets the value indicating whether the setup has been completed.
    /// </summary>
    public bool? IsDone
    {
        get { return this.isDone; }
        private set { this.SetProperty(ref this.isDone, value); }
    }

    /// <summary>
    /// Gets the default server folder path.
    /// </summary>
    public string DefaultServerFolderPath => GeneralSettings.DefaultServersPath;

    /// <summary>
    /// Gets or sets the server folder path the user enters as the preferred folder to store their servers.
    /// </summary>
    public string CustomServerFolderPath
    {
        get => this.customServerFolderPath;
        set
        {
            this.SetProperty(ref this.customServerFolderPath, value.Trim());
            this.RootServerFolderConfirmCommand.NotifyCanExecuteChanged();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the program should use a custom path.
    /// </summary>
    public bool ShouldUseCustomPath
    {
        get => this.shouldUseCustom;
        set
        {
            this.SetProperty(ref this.shouldUseCustom, value);
            this.RootServerFolderConfirmCommand.NotifyCanExecuteChanged();
        }
    }

    /// <summary>
    /// Saves the given settings.
    /// </summary>
    public void FinishUp()
    {
        AppSettings.GeneralSettings = new GeneralSettings();
        AppSettings.GeneralSettings.ServerFoldersPath = this.ShouldUseCustomPath ? this.CustomServerFolderPath : this.DefaultServerFolderPath;
        AppSettings.GeneralSettings.Save();

        this.IsDone = true;
    }

    private void SelectFolder()
    {
        string selected = Ioc.Default.GetService<IFolderSelectorService>().SelectFolder("Please select an empty folder to store your servers in");

        if (!string.IsNullOrEmpty(selected))
        {
            this.CustomServerFolderPath = selected;
        }
    }

    /// <summary>
    /// Called when the user confirms the server folder.
    /// </summary>
    private void ConfirmRootServerFolder()
    {
        string folderPath = this.ShouldUseCustomPath ? this.CustomServerFolderPath : this.DefaultServerFolderPath;

        try
        {
            Ioc.Default.GetService<IBasicInfoManagerService>().CreateRootServerFolder(folderPath);
        }
        catch (Exception e)
        {
            this.ErrorViewModel = new ErrorViewModel($"Failed to create folder {folderPath} for the following reason: {e.Message}. Please try a different directory.");
            return;
        }

        this.FinishUp();
    }
}
