// <copyright file="FirstStartWindowVM.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using TTServerMaker.Engine;
    using TTServerMaker.Engine.Services;

    /// <summary>
    /// The view model of the initial program setup.
    /// </summary>
    public class FirstStartWindowVM : ViewModelBase
    {
        private bool shouldUseCustom;
        private string customServerFolderPath;
        private ErrorViewModel errorViewModel;
        private bool? isDone;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstStartWindowVM"/> class.
        /// </summary>
        public FirstStartWindowVM()
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
        public ErrorViewModel ErrorViewModel { get => this.errorViewModel; set => this.Set(ref this.errorViewModel, value); }

        /// <summary>
        /// Gets or sets a value indicating whether the setup has been completed.
        /// </summary>
        public bool? IsDone
        {
            get { return this.isDone; }
            set { this.Set(ref this.isDone, value); }
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
                this.Set(ref this.customServerFolderPath, value.Trim());
                this.RootServerFolderConfirmCommand.RaiseCanExecuteChanged();
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
                this.Set(ref this.shouldUseCustom, value);
                this.RootServerFolderConfirmCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Saves the given settings.
        /// </summary>
        public void FinishUp()
        {
            AppSettings.GeneralSettings = new Engine.GeneralSettings();
            AppSettings.GeneralSettings.ServerFoldersPath = this.ShouldUseCustomPath ? this.CustomServerFolderPath : this.DefaultServerFolderPath;
            AppSettings.GeneralSettings.Save();

            this.IsDone = true;
        }

        private void SelectFolder()
        {
            string selected = SimpleIoc.Default.GetInstance<IFolderSelectorService>().SelectFolder("Please select an empty folder to store your servers in");

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
                ServiceLocator.Current.GetInstance<IServerInfoLoadingService>().CreateRootServerFolder(folderPath);
            }
            catch (Exception e)
            {
                this.ErrorViewModel = new ErrorViewModel($"Failed to create folder {folderPath} for the following reason: {e.Message}. Please try a different directory.");
                return;
            }

            this.FinishUp();
        }
    }
}
