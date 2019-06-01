namespace ServerEngine.Models.Servers
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
