using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class UShortRangeTests
    {
        [Test]
        public void Test_Extend1()
        {
            IValueSpace<ushort> range = RangeRecordFactory.CreateRange<ushort>(0);
            range.Extend(10);
            Assert.IsTrue(range.Contains(0));
            Assert.IsTrue(range.Contains(5));
            Assert.IsTrue(range.Contains(10));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(11));
        }

        [Test]
        public void Test_Extend2()
        {
            IValueSpace<ushort> range = RangeRecordFactory.CreateRange<ushort>(ushort.MaxValue);
            range.Extend(10);
            Assert.IsTrue(range.Contains(ushort.MaxValue));
            Assert.IsTrue(range.Contains(500));
            Assert.IsTrue(range.Contains(1068));
            Assert.IsFalse(range.Contains(9));
        }

        [Test]
        public void Tests1()
        {
            IValueSpace<ushort> range = RangeRecordFactory.CreateRange<ushort>(0);
            Assert.IsTrue(range.Contains(0));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(1));
        }

        [Test]
        public void Tests2()
        {
            IValueSpace<ushort> onlyTrue = RangeRecordFactory.CreateRange<ushort>(1178);
            Assert.IsTrue(onlyTrue.Contains(1178));
            Assert.IsFalse(onlyTrue.Contains(1179));
            Assert.IsFalse(onlyTrue.Contains(1177));
        }
    }
}