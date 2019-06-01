using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

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
        [JsonIgnore]
        public int NumberRepresentation
        {
            get
            {
                int Charvalue = char.ToUpper(Character) - 64;
                return Year * 10000 + Week * 100 + Charvalue;
            }
        }

        [JsonConstructor]
        public VanillaSnapshotVersion(string VersionString) : base(VersionString)
        {
            if (string.IsNullOrEmpty(VersionString))
                return;

            // Making sure the letters are lowercase
            VersionString = VersionString.ToLower();

            // Getting the info out of the string for easier comparison
            Match match = new Regex(@"^(?<year>\d{2})w(?<month>\d{2})(?<character>[a-z])$").Match(VersionString);

            if (!match.Success)
                throw new ArgumentException($"Failed to match the snapshot string ({VersionString}) to the snapshot format.", VersionString);

            Year = Convert.ToInt32(match.Groups["year"].Value);
            Week = Convert.ToInt32(match.Groups["month"].Value);
            Character = match.Groups["character"].Value[0];
        }

      
        public override int CompareTo(VersionBase other)
        {
            if (!(other is VanillaSnapshotVersion))
                return 0;

            if (NumberRepresentation < (other as VanillaSnapshotVersion).NumberRepresentation)
                return -1;
            if (NumberRepresentation > (other as VanillaSnapshotVersion).NumberRepresentation)
                return 1;
            return 0;
        }

    }
}
