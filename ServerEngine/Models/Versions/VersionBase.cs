using System;
using Newtonsoft.Json;

namespace ServerEngine.Models.Versions
{
    [JsonObject(MemberSerialization.OptIn)]
    abstract public class VersionBase : IComparable<VersionBase>
    {
        [JsonProperty]
        public string VersionString { get; set; }

        public abstract int CompareTo(VersionBase other);

        public VersionBase(string VersionString)
        {
            this.VersionString = VersionString;
        }

        public override string ToString()
        {
            return VersionString;
        }

        public static bool operator <(VersionBase b, VersionBase c)
        {
            return b.CompareTo(c) == -1;
        }

        public static bool operator >(VersionBase b, VersionBase c)
        {
            return b.CompareTo(c) == 1;
        }
    }
}
