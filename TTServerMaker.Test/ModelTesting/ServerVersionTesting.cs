namespace TTServerMaker.WPF.Tests.ModelTesting
{
    using System;
    using NUnit.Framework;
    using TTServerMaker.Engine.Models.Versions;

    public class ServerVersionTesting
    {
        public class ServerVersionComparison
        {
            public class VanillaServerVersionComparison
            {
                [TestCase]
                public void TestSameVersions()
                {
                    VanillaOfficialVersion same = new VanillaOfficialVersion("1.2.3");
                    VanillaOfficialVersion another = new VanillaOfficialVersion("1.2.3");

                    Assert.AreEqual(same.CompareTo(another), 0);
                }

                [TestCase]
                public void OneShorter()
                {
                    VanillaOfficialVersion smaller = new VanillaOfficialVersion("1.2");
                    VanillaOfficialVersion bigger = new VanillaOfficialVersion("1.2.1");

                    CompareBothWays(smaller, bigger);
                }

                [TestCase]
                public void Normal()
                {
                    VanillaOfficialVersion smaller = new VanillaOfficialVersion("1.2.4");
                    VanillaOfficialVersion bigger = new VanillaOfficialVersion("1.12.1");

                    CompareBothWays(smaller, bigger);
                    Assert.IsTrue(smaller < bigger);
                }

                [TestCase]
                public void FirstNumberDifferent()
                {
                    VanillaOfficialVersion smaller = new VanillaOfficialVersion("1.2.4" );
                    VanillaOfficialVersion bigger = new VanillaOfficialVersion("2.1" );

                    CompareBothWays(smaller, bigger);

                    Assert.IsTrue(smaller < bigger);
                }

                [TestCase]
                public void SameVersions()
                {
                    VanillaSnapshotVersion same = new VanillaSnapshotVersion("11w10a");
                    VanillaSnapshotVersion another = new VanillaSnapshotVersion("11w10a");

                    Assert.AreEqual(same.CompareTo(another), 0);
                }

                [TestCase]
                public void DifferentCharacters()
                {
                    VanillaSnapshotVersion some = new VanillaSnapshotVersion("19w03b");
                    VanillaSnapshotVersion another = new VanillaSnapshotVersion("19w03c");

                    CompareBothWays(some, another);
                }

                [TestCase]
                public void NewYear()
                {
                    VanillaSnapshotVersion some = new VanillaSnapshotVersion("19w53b");
                    VanillaSnapshotVersion another = new VanillaSnapshotVersion("20w01a");

                    CompareBothWays(some, another);
                }

                [TestCase]
                public void NextWeek()
                {
                    VanillaSnapshotVersion some = new VanillaSnapshotVersion("19w01z");
                    VanillaSnapshotVersion another = new VanillaSnapshotVersion("19w02a");

                    CompareBothWays(some, another);
                }

                [TestCase]
                /// <summary>
                /// Compare two versions both ways using the .CompareTo function. Only works if the first is smaller
                /// </summary>
                /// <param name="smaller"></param>
                /// <param name="bigger"></param>
                private static void CompareBothWays(ServerVersion smaller, ServerVersion bigger)
                {
                    Assert.That(smaller, Is.LessThan(bigger));
                    Assert.That(bigger, Is.GreaterThan(smaller));
                }
            }
        }
    }
}
