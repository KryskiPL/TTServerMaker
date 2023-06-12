// <copyright file="ServerSelectWindowVM.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using CommunityToolkit.Mvvm.DependencyInjection;
    using TTServerMaker.Engine.Factories;
    using TTServerMaker.Engine.Models.Servers;
    using TTServerMaker.Engine.Services;

    /// <summary>
    /// The view model of selecting a server.
    /// </summary>
    public class ServerSelectWindowVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSelectWindowVM"/> class.
        /// </summary>
        public ServerSelectWindowVM()
        {
            this.ServerInfoManager = Ioc.Default.GetService<IBasicInfoManagerService>();

            this.LoadServers();
        }

        /// <summary>
        /// Gets the server info the user selected to load up.
        /// </summary>
        public BasicInfo SelectedServer { get; private set; }

        /// <summary>
        /// Gets or sets the list of the servers.
        /// </summary>
        public ObservableCollection<BasicInfo> ServerInfoList { get; set; }

        private IBasicInfoManagerService ServerInfoManager { get; }

        private void LoadServers()
        {
            Task<ObservableCollection<BasicInfo>> task = new Task<ObservableCollection<BasicInfo>>(() =>
            new ObservableCollection<BasicInfo>(this.ServerInfoManager.GetServerInfos()));

            task.ContinueWith((a) =>
            {
                this.ServerInfoList = a.Result;
                this.ServerInfoList.Add(new BasicInfo { Name = "My first server", ServerType = ServerType.Vanilla, DateLastLoaded = DateTime.Now });
            });
            task.Start();
        }
    }
}