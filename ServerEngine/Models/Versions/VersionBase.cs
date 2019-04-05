using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerEngine.Models.Versions
{
    abstract public class VersionBase : IComparable<VersionBase>
    {
        public string VersionString { get; set; }

        public string ServerTypeStr { get { return this.ToString(); }  }

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
