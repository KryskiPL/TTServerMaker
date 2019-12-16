// <copyright file="ForgeServer.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.Engine.Models.Servers
{
    public class ForgeServer : ServerBase
    {
        public ForgeServer(BasicServerInfo basicServerInfo) : base(basicServerInfo)
        {
        }

        public override string ServerTypeStr
        {
            get { return "Forge"; }
        }
    }
}