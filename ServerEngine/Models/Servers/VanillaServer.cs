namespace ServerEngine.Models.Servers
{
    public class VanillaServer : ServerBase
    {
        public VanillaServer(BasicServerInfo basicServerInfo) : base (basicServerInfo)
        {

        }


        public override string ServerTypeStr
        {
            get { return "Vanilla"; }
        }
    }
}
