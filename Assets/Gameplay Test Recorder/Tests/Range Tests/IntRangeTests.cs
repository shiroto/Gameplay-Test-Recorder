using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class IntRangeTests
    {
        [Test]
        public void TestsInt_Extend1()
        {
            IValueSpace<int> range = RangeRecordFactory.CreateRange<int>(0);
            range.Extend(10);
            Assert.IsTrue(range.Contains(0));
            Assert.IsTrue(range.Contains(5));
            Assert.IsTrue(range.Contains(10));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(11));
        }

        [Test]
        public void TestsInt_Extend2()
        {
            IValueSpace<int> range = RangeRecordFactory.CreateRange<int>(1068);
            range.Extend(10);
            Assert.IsTrue(range.Contains(10));
            Assert.IsTrue(range.Contains(500));
            Assert.IsTrue(range.Contains(1068));
            Assert.IsFalse(range.Contains(9));
            Assert.IsFalse(range.Contains(1069));
        }

        [Test]
        public void TestsInt1()
        {
            IValueSpace<int> range = RangeRecordFactory.CreateRange<int>(0);
            Assert.IsTrue(range.Contains(0));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(1));
        }

        [Test]
        public void TestsInt2()
        {
            IValueSpace<int> onlyTrue = RangeRecordFactory.CreateRange<int>(1178);
            Assert.IsTrue(onlyTrue.Contains(1178));
            Assert.IsFalse(onlyTrue.Contains(1179));
            Assert.IsFalse(onlyTrue.Contains(1177));
        }
    }
}