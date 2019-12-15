using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TTServerMaker.ServerEngine.Models.Versions;

namespace TTServerMaker.WPF.Tests.ModelTesting
{
    [TestClass]
    public class ServerVersionTesting
    {
        [TestClass]
        public class ServerVersionComparison
        {
            [TestClass]
            public class VanillaServerVersionComparison
            {
                /// <summary>
                /// Compare two versions both ways using the .CompareTo function. Only works if the first is smaller
                /// </summary>
                /// <param name="Smaller"></param>
                /// <param name="Bigger"></param>
                private void CompareBothWays(ServerVersion Smaller, ServerVersion Bigger)
                {
                    Assert.AreEqual(Smaller.CompareTo(Bigger), -1);
                    Assert.AreEqual(Bigger.CompareTo(Smaller), 1);
                }


                #region Official
                [TestMethod]
                public void TestSameVersions()
                {
                    VanillaOfficialVersion same = new VanillaOfficialVersion("1.2.3");
                    VanillaOfficialVersion another = new VanillaOfficialVersion("1.2.3");

                    Assert.AreEqual(same.CompareTo(another), 0);
                }

                [TestMethod]
                public void OneShorter()
                {
                    VanillaOfficialVersion smaller = new VanillaOfficialVersion("1.2");
                    VanillaOfficialVersion bigger = new VanillaOfficialVersion("1.2.1");

                    CompareBothWays(smaller, bigger);
                }

                [TestMethod]
                public void Normal()
                {
                    VanillaOfficialVersion smaller = new VanillaOfficialVersion("1.2.4");
                    VanillaOfficialVersion bigger = new VanillaOfficialVersion("1.12.1");

                    CompareBothWays(smaller, bigger);
                    Assert.IsTrue(smaller < bigger);
                }

                [TestMethod]
                public void FirstNumberDifferent()
                {
                    VanillaOfficialVersion smaller = new VanillaOfficialVersion("1.2.4" );
                    VanillaOfficialVersion bigger = new VanillaOfficialVersion("2.1" );

                    CompareBothWays(smaller, bigger);

                    Assert.IsTrue(smaller < bigger);
                }



                #endregion

                #region Snapshot
                [TestMethod]
                public void SameVersions()
                {
                    VanillaSnapshotVersion same = new VanillaSnapshotVersion("11w10a");
                    VanillaSnapshotVersion another = new VanillaSnapshotVersion("11w10a");

                    Assert.AreEqual(same.CompareTo(another), 0);
                }

                [TestMethod]
                public void DifferentCharacters()
                {
                    VanillaSnapshotVersion some = new VanillaSnapshotVersion("19w03b");
                    VanillaSnapshotVersion another = new VanillaSnapshotVersion("19w03c");

                    CompareBothWays(some, another);
                }

                [TestMethod]
                public void NewYear()
                {
                    VanillaSnapshotVersion some = new VanillaSnapshotVersion("19w53b");
                    VanillaSnapshotVersion another = new VanillaSnapshotVersion("20w01a");

                    CompareBothWays(some, another);
                }

                [TestMethod]
                public void NextWeek()
                {
                    VanillaSnapshotVersion some = new VanillaSnapshotVersion("19w01z");
                    VanillaSnapshotVersion another = new VanillaSnapshotVersion("19w02a");

                    CompareBothWays(some, another);
                }
                #endregion
            }
        }
    }
}
