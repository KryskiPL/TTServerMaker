using Newtonsoft.Json;
using System;

namespace TTServerMaker.Engine.Models.Versions
{
    /// <summary>
    /// The official vanilla server type.
    /// </summary>
    public class VanillaOfficialVersion : ServerVersion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VanillaOfficialVersion"/> class.
        /// </summary>
        /// <param name="versionString">The string that represents the version.</param>
        [JsonConstructor]
        public VanillaOfficialVersion(string versionString)
            : base(versionString) { }

        /// <inheritdoc/>
        public override int CompareTo(ServerVersion other)
        {
            /* Comparing Vanilla version numbers
             *
             * As of 2019, all official releases (after beta) have followed the version format like 1.12.2
             * Some versions have less numbers, like 1.12, which basically means 1.12.0
             *
             * Comparing two versions only needs to test which version has a bigger number first, or is longer
             */
            if (!(other is VanillaOfficialVersion))
            {
                return 0;
            }

            string[] thisSpl = VersionString.Split('.');
            string[] otherSpl = other.VersionString.Split('.');

            for (int i = 0; i < Math.Min(thisSpl.Length, otherSpl.Length); i++)
            {
                int thisNum = Convert.ToInt32(thisSpl[i]);
                int otherNum = Convert.ToInt32(otherSpl[i]);

                if (thisNum < otherNum)
                {
                    return -1;
                }

                if (thisNum > otherNum)
                {
                    return 1;
                }
            }

            // If the code got to this point, that means that the two strings are identical until one was shorter
            // That will be the smaller one
            if (thisSpl.Length < otherSpl.Length)
            {
                return -1;
            }

            if (thisSpl.Length > otherSpl.Length)
            {
                return 1;
            }

            return 0;
        }
    }
}