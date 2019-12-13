namespace TTServerMaker.ServerEngine.Models.Versions
{
    using Newtonsoft.Json;
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Snapshot version of the vanilla game.
    /// </summary>
    public class VanillaSnapshotVersion : ServerVersion
    {
        /// <summary>
        /// Gets or sets the year part of the snapshot version.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the week part of the snapshot version.
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// Gets or sets the character at the end of the snapshot version.
        /// </summary>
        public char Character { get; set; }

        /// <summary>
        /// Gives a number representation to the snapshot's version. Makes it easy to compare them.
        /// </summary>
        [JsonIgnore]
        public int NumberRepresentation => (this.Year * 10000) + (this.Week * 100) + char.ToUpper(this.Character) - 64;

        /// <summary>
        /// Initializes a new instance of the <see cref="VanillaSnapshotVersion"/> class.
        /// </summary>
        /// <param name="versionString">The string repsresenting the snapshot version.</param>
        [JsonConstructor]
        public VanillaSnapshotVersion(string versionString)
            : base(versionString)
        {
            if (string.IsNullOrEmpty(versionString))
            {
                return;
            }

            // Making sure the letters are lowercase
            versionString = versionString.ToLower();

            // Getting the info out of the string for easier comparison
            Match match = new Regex(@"^(?<year>\d{2})w(?<month>\d{2})(?<character>[a-z])$").Match(versionString);

            if (!match.Success)
            {
                throw new ArgumentException($"Failed to match the snapshot string ({versionString}) to the snapshot format.", versionString);
            }

            this.Year = Convert.ToInt32(match.Groups["year"].Value);
            this.Week = Convert.ToInt32(match.Groups["month"].Value);
            this.Character = match.Groups["character"].Value[0];
        }

        /// <inheritdoc/>
        public override int CompareTo(ServerVersion other)
        {
            if (!(other is VanillaSnapshotVersion))
            {
                return 0;
            }

            if (this.NumberRepresentation < (other as VanillaSnapshotVersion).NumberRepresentation)
            {
                return -1;
            }

            if (this.NumberRepresentation > (other as VanillaSnapshotVersion).NumberRepresentation)
            {
                return 1;
            }

            return 0;
        }
    }
}