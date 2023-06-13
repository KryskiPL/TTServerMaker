// <copyright file="ServerVersionTesting.cs" company="TThread">
// Copyright (c) TThread. All rights reserved.
// </copyright>

namespace TTServerMaker.WPF.Tests.ModelTesting;

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
                VanillaOfficialVersion same = new ("1.2.3");
                VanillaOfficialVersion another = new ("1.2.3");

                Assert.AreEqual(same.CompareTo(another), 0);
            }

            [TestCase]
            public void OneShorter()
            {
                VanillaOfficialVersion smaller = new ("1.2");
                VanillaOfficialVersion bigger = new ("1.2.1");

                CompareBothWays(smaller, bigger);
            }

            [TestCase]
            public void Normal()
            {
                VanillaOfficialVersion smaller = new ("1.2.4");
                VanillaOfficialVersion bigger = new ("1.12.1");

                CompareBothWays(smaller, bigger);
                Assert.IsTrue(smaller < bigger);
            }

            [TestCase]
            public void FirstNumberDifferent()
            {
                VanillaOfficialVersion smaller = new ("1.2.4");
                VanillaOfficialVersion bigger = new ("2.1");

                CompareBothWays(smaller, bigger);

                Assert.IsTrue(smaller < bigger);
            }

            [TestCase]
            public void SameVersions()
            {
                VanillaSnapshotVersion same = new ("11w10a");
                VanillaSnapshotVersion another = new ("11w10a");

                Assert.AreEqual(same.CompareTo(another), 0);
            }

            [TestCase]
            public void DifferentCharacters()
            {
                VanillaSnapshotVersion some = new ("19w03b");
                VanillaSnapshotVersion another = new ("19w03c");

                CompareBothWays(some, another);
            }

            [TestCase]
            public void NewYear()
            {
                VanillaSnapshotVersion some = new ("19w53b");
                VanillaSnapshotVersion another = new ("20w01a");

                CompareBothWays(some, another);
            }

            [TestCase]
            public void NextWeek()
            {
                VanillaSnapshotVersion some = new ("19w01z");
                VanillaSnapshotVersion another = new ("19w02a");

                CompareBothWays(some, another);
            }

            /// <summary>
            /// Compare two versions both ways using the .CompareTo function. Asserts that the first is smaller.
            /// </summary>
            /// <param name="smaller">The smaller version.</param>
            /// <param name="bigger">The bigger version.</param>
            private static void CompareBothWays(ServerVersion smaller, ServerVersion bigger)
            {
                Assert.That(smaller, Is.LessThan(bigger));
                Assert.That(bigger, Is.GreaterThan(smaller));
            }
        }
    }
}
