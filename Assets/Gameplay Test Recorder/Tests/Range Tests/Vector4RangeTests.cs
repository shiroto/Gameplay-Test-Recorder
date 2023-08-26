using NUnit.Framework;
using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class Vector4RangeTests
    {
        [Test]
        public void Test_1()
        {
            IValueSpace<Vector4> range = RangeRecordFactory.CreateRange<Vector4>(Vector4.zero);
            Assert.IsTrue(range.Contains(Vector4.zero));
            Assert.IsFalse(range.Contains(new Vector4(-1, 0, 0, 0)));
            Assert.IsFalse(range.Contains(new Vector4(0, -1, 0, 0)));
        }

        [Test]
        public void Test_2()
        {
            IValueSpace<Vector4> range = RangeRecordFactory.CreateRange<Vector4>(new Vector4(10, 20.5f, 0, 0));
            Assert.IsTrue(range.Contains(new Vector4(10, 20.5f, 0, 0)));
            Assert.IsFalse(range.Contains(new Vector4(11, 20.5f, 0, 0)));
            Assert.IsFalse(range.Contains(new Vector4(10, 20.4f, 0, 0)));
        }

        [Test]
        public void Test_Extend_Radius()
        {
            IValueSpace<Vector4> range = RangeRecordFactory.CreateRange<Vector4>(Vector4.zero);
            range.Extend(new Vector4(10, 0, 0));
            Assert.IsTrue(range.Contains(new Vector4(0, 0, 0, 0)));
            Assert.IsTrue(range.Contains(new Vector4(10, 0, 0, 0)));
            Assert.IsTrue(range.Contains(new Vector4(-10, 0, 0, 0)));
            Assert.IsTrue(range.Contains(new Vector4(0, 10, 0, 0)));
            Assert.IsTrue(range.Contains(new Vector4(0, -10, 0, 0)));
            Assert.IsTrue(range.Contains(new Vector4(0, 0, 10, 0)));
            Assert.IsTrue(range.Contains(new Vector4(0, 0, -10, 0)));
            Assert.IsTrue(range.Contains(new Vector4(0, 0, 0, 10)));
            Assert.IsTrue(range.Contains(new Vector4(0, 0, 0, -10)));
            Assert.IsTrue(range.Contains(new Vector4(9, 1, 0, 0)));
            Assert.IsTrue(range.Contains(new Vector4(-5, 5, 0, 0)));
            Assert.IsFalse(range.Contains(new Vector4(11, 0, 0, 0)));
            Assert.IsFalse(range.Contains(new Vector4(0, 11, 0, 0)));
            Assert.IsFalse(range.Contains(new Vector4(0, 0, 11, 0)));
        }
    }
}