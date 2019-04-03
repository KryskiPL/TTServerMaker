using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ServerEngine.Models.Versions
{
    public class VanillaSnapshotVersion : VersionBase
    {
        public int Year { get; set; }
        public int Week { get; set; }
        public char Character { get; set; }

        /// <summary>
        /// Gives a number representation to the snapshot's version. Makes it easy to compare them.
        /// </summary>
        public int NumberRepresentation
        {
            get
            {
                int Charvalue = char.ToUpper(Character) - 64;
                return Year * 10000 + Week * 100 + Charvalue;
            }
        }

        public VanillaSnapshotVersion(string SnapshotVersionString) : base(SnapshotVersionString)
        {
            // Making sure the letters are lowercase
            SnapshotVersionString = SnapshotVersionString.ToLower();

            // Getting the info out of the string for easier comparison
            Match match = new Regex(@"^(?<year>\d{2})w(?<month>\d{2})(?<character>[a-z])$").Match(SnapshotVersionString);

            if (!match.Success)
                throw new ArgumentException($"Failed to match the snapshot string ({SnapshotVersionString}) to the snapshot format.", SnapshotVersionString);

            this.Year = Convert.ToInt32(match.Groups["year"].Value);
            this.Week = Convert.ToInt32(match.Groups["month"].Value);
            this.Character = match.Groups["character"].Value[0];
        }

        public override int CompareTo(VersionBase other)
        {
            if (!(other is VanillaSnapshotVersion))
                return 0;

            if (this.NumberRepresentation < (other as VanillaSnapshotVersion).NumberRepresentation)
                return -1;
            else if (this.NumberRepresentation > (other as VanillaSnapshotVersion).NumberRepresentation)
                return 1;
            else return 0;
        }

    }
}
