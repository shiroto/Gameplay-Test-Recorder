using NUnit.Framework;
using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class Vector3RangeTests
    {
        [Test]
        public void Test_1()
        {
            IValueSpace<Vector3> range = RangeRecordFactory.CreateRange<Vector3>(Vector3.zero);
            Assert.IsTrue(range.Contains(Vector3.zero));
            Assert.IsFalse(range.Contains(new Vector3(-1, 0, 0)));
            Assert.IsFalse(range.Contains(new Vector3(0, -1, 0)));
        }

        [Test]
        public void Test_2()
        {
            IValueSpace<Vector3> range = RangeRecordFactory.CreateRange<Vector3>(new Vector3(10, 20.5f, 0));
            Assert.IsTrue(range.Contains(new Vector3(10, 20.5f, 0)));
            Assert.IsFalse(range.Contains(new Vector3(11, 20.5f, 0)));
            Assert.IsFalse(range.Contains(new Vector3(10, 20.4f, 0)));
        }

        [Test]
        public void Test_Extend_Radius()
        {
            IValueSpace<Vector3> range = RangeRecordFactory.CreateRange<Vector3>(Vector3.zero);
            range.Extend(new Vector3(10, 0, 0));
            Assert.IsTrue(range.Contains(new Vector3(0, 0, 0)));
            Assert.IsTrue(range.Contains(new Vector3(10, 0, 0)));
            Assert.IsTrue(range.Contains(new Vector3(-10, 0, 0)));
            Assert.IsTrue(range.Contains(new Vector3(0, 10, 0)));
            Assert.IsTrue(range.Contains(new Vector3(0, -10, 0)));
            Assert.IsTrue(range.Contains(new Vector3(0, 0, 10)));
            Assert.IsTrue(range.Contains(new Vector3(0, 0, -10)));
            Assert.IsTrue(range.Contains(new Vector3(9, 1, 0)));
            Assert.IsTrue(range.Contains(new Vector3(-5, 5, 0)));
            Assert.IsFalse(range.Contains(new Vector3(11, 0, 0)));
            Assert.IsFalse(range.Contains(new Vector3(0, 11, 0)));
            Assert.IsFalse(range.Contains(new Vector3(0, 0, 11)));
        }
    }
}