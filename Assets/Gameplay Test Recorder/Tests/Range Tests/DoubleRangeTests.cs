using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class DoubleRangeTests
    {
        [Test]
        public void TestsDouble_Extend1()
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(0);
            range.Extend(10);
            Assert.IsTrue(range.Contains(0));
            Assert.IsTrue(range.Contains(5));
            Assert.IsTrue(range.Contains(10));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(11));
        }

        [Test]
        public void TestsDouble_Extend2()
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(68.4567);
            range.Extend(10);
            Assert.IsTrue(range.Contains(10));
            Assert.IsTrue(range.Contains(68.4567));
            Assert.IsTrue(range.Contains(68.56 / 2.0765));
            Assert.IsFalse(range.Contains(9));
            Assert.IsFalse(range.Contains(68.4568));
        }

        [Test]
        public void TestsDouble1()
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(0);
            Assert.IsTrue(range.Contains(0));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(1));
        }

        [Test]
        public void TestsDouble2()
        {
            IValueSpace<double> onlyTrue = RangeRecordFactory.CreateRange<double>(8652.563);
            Assert.IsTrue(onlyTrue.Contains(8652.563));
            Assert.IsFalse(onlyTrue.Contains(8652.562));
            Assert.IsFalse(onlyTrue.Contains(8652.564));
        }
    }
}