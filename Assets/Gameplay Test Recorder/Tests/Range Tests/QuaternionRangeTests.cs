using NUnit.Framework;
using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class QuaternionRangeTests
    {
        [Test]
        public void Test_1()
        {
            IValueSpace<Quaternion> range = RangeRecordFactory.CreateRange<Quaternion>(Quaternion.identity);
            Assert.IsTrue(range.Contains(Quaternion.identity));
            Assert.IsFalse(range.Contains(new Quaternion(-1, 0, 0, 0)));
            Assert.IsFalse(range.Contains(new Quaternion(0, -1, 0, 0)));
        }

        [Test]
        public void Test_2()
        {
            IValueSpace<Quaternion> range = RangeRecordFactory.CreateRange<Quaternion>(Quaternion.Euler(10, 20.5f, 0));
            Assert.IsTrue(range.Contains(Quaternion.Euler(10, 20.5f, 0)));
            Assert.IsFalse(range.Contains(Quaternion.Euler(11, 20.5f, 0)));
            Assert.IsFalse(range.Contains(Quaternion.Euler(10, 20f, 0)));
        }

        [Test]
        public void Test_Extend_Radius()
        {
            IValueSpace<Quaternion> range = RangeRecordFactory.CreateRange<Quaternion>(Quaternion.identity);
            range.Extend(Quaternion.Euler(10, 0, 0));
            Assert.IsTrue(range.Contains(Quaternion.Euler(10, 0, 0)));
            Assert.IsTrue(range.Contains(Quaternion.Euler(0, 10, 0)));
            Assert.IsTrue(range.Contains(Quaternion.Euler(0, 0, 10)));
            Assert.IsTrue(range.Contains(Quaternion.Euler(-10, 0, 0)));
            Assert.IsTrue(range.Contains(Quaternion.Euler(0, -10, 0)));
            Assert.IsTrue(range.Contains(Quaternion.Euler(0, 0, -10)));
            Assert.IsTrue(range.Contains(Quaternion.Euler(1, 1, 1)));
            Assert.IsFalse(range.Contains(Quaternion.Euler(0, 0, 20)));
            Assert.IsFalse(range.Contains(Quaternion.Euler(0, 10, 10)));
        }
    }
}