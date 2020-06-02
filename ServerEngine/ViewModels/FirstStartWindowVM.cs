namespace TTServerMaker.Engine.ViewModels
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TTServerMaker.Engine;

    public class FirstStartWindowVM
    {
        /// <summary>
        /// Gets the default server folder path.
        /// </summary>
        public string DefaultServerFolderPath => GeneralSettings.DefaultServersPath;

        /// <summary>
        /// The server folder path the user enters as the preferred folder to store their servers.
        /// </summary>
        public string CustomServerFolderPath { get; set; }
    }
}
