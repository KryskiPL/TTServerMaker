using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerEngine.Models.Versions
{
    public class VanillaOfficialVersion : VersionBase
    {
        public VanillaOfficialVersion(string SnapshotVersionString) : base (SnapshotVersionString) { }

        public override int CompareTo(VersionBase other)
        {
            /* Comparing Vanilla version numbers
             * 
             * As of 2019, all official releases (after beta) have followed the version format like 1.12.2
             * Some versions have less numbers, like 1.12, which basically means 1.12.0
             * 
             * Comparing two versions only needs to test which version has a bigger number first, or is longer
             */
            if (!(other is VanillaOfficialVersion))
                return 0;

            string[] thisSpl = this.VersionString.Split('.');
            string[] otherSpl = other.VersionString.Split('.');

            for(int i = 0; i < Math.Min(thisSpl.Length, otherSpl.Length); i++)
            {
                int thisNum = Convert.ToInt32(thisSpl[i]);
                int otherNum = Convert.ToInt32(otherSpl[i]);

                if (thisNum < otherNum)
                    return -1;
                else if (thisNum > otherNum)
                    return 1;
            }

            // If the code got to this point, that means that the two strings are identical until one was shorter
            // That will be the smaller one

            if (thisSpl.Length < otherSpl.Length)
                return -1;
            else if (thisSpl.Length > otherSpl.Length)
                return 1;
            else return 0;
        }
    }
}
