using NUnit.Framework;
using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class Vector2RangeTests
    {
        [Test]
        public void Test_1()
        {
            IValueSpace<Vector2> range = RangeRecordFactory.CreateRange<Vector2>(Vector2.zero);
            Assert.IsTrue(range.Contains(Vector2.zero));
            Assert.IsFalse(range.Contains(new Vector2(-1, 0)));
            Assert.IsFalse(range.Contains(new Vector2(0, -1)));
        }

        [Test]
        public void Test_2()
        {
            IValueSpace<Vector2> range = RangeRecordFactory.CreateRange<Vector2>(new Vector2(10, 20.5f));
            Assert.IsTrue(range.Contains(new Vector2(10, 20.5f)));
            Assert.IsFalse(range.Contains(new Vector2(11, 20.5f)));
            Assert.IsFalse(range.Contains(new Vector2(10, 20.4f)));
        }

        [Test]
        public void Test_Extend_Radius()
        {
            IValueSpace<Vector2> range = RangeRecordFactory.CreateRange<Vector2>(Vector2.zero);
            range.Extend(new Vector2(10, 0));
            Assert.IsTrue(range.Contains(new Vector2(0, 0)));
            Assert.IsTrue(range.Contains(new Vector2(10, 0)));
            Assert.IsTrue(range.Contains(new Vector2(-10, 0)));
            Assert.IsTrue(range.Contains(new Vector2(0, 10)));
            Assert.IsTrue(range.Contains(new Vector2(0, -10)));
            Assert.IsTrue(range.Contains(new Vector2(9, 1)));
            Assert.IsTrue(range.Contains(new Vector2(-5, 5)));
            Assert.IsFalse(range.Contains(new Vector2(11, 0)));
            Assert.IsFalse(range.Contains(new Vector2(0, 11)));
        }
    }
}