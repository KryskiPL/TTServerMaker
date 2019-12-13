namespace TTServerMaker.ServerEngine.Models.Servers
{
    /// <summary>
    /// The vanillia minecraft server.
    /// </summary>
    public class VanillaServer : ServerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VanillaServer"/> class.
        /// </summary>
        /// <param name="basicServerInfo">The basic server info.</param>
        public VanillaServer(BasicServerInfo basicServerInfo)
            : base(basicServerInfo)
        {
        }

        public override string ServerTypeStr
        {
            get { return "Vanilla"; }
        }
    }
}