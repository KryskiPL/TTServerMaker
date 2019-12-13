namespace TTServerMaker.ServerEngine.Models.Versions
{
    using Newtonsoft.Json;
    using System;

    [JsonObject(MemberSerialization.OptIn)]
    abstract public class ServerVersion : IComparable<ServerVersion>
    {
        [JsonProperty]
        public string VersionString { get; set; }

        public abstract int CompareTo(ServerVersion other);

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerVersion"/> class.
        /// </summary>
        /// <param name="VersionString"></param>
        public ServerVersion(string VersionString)
        {
            this.VersionString = VersionString;
        }

        public static bool operator <(ServerVersion b, ServerVersion c)
        {
            return b.CompareTo(c) == -1;
        }

        public static bool operator >(ServerVersion b, ServerVersion c)
        {
            return b.CompareTo(c) == 1;
        }

        public override string ToString()
        {
            return VersionString;
        }
    }
}