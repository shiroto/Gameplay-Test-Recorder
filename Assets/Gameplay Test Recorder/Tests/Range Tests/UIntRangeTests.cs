using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class UIntRangeTests
    {
        [Test]
        public void TestsInt_Extend1()
        {
            IValueSpace<uint> range = RangeRecordFactory.CreateRange<uint>(0);
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
            IValueSpace<uint> range = RangeRecordFactory.CreateRange<uint>(uint.MaxValue);
            range.Extend(10);
            Assert.IsTrue(range.Contains(uint.MaxValue));
            Assert.IsTrue(range.Contains(500));
            Assert.IsTrue(range.Contains(1068));
            Assert.IsFalse(range.Contains(9));
        }

        [Test]
        public void TestsInt1()
        {
            IValueSpace<uint> range = RangeRecordFactory.CreateRange<uint>(0);
            Assert.IsTrue(range.Contains(0));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(1));
        }

        [Test]
        public void TestsInt2()
        {
            IValueSpace<uint> onlyTrue = RangeRecordFactory.CreateRange<uint>(1178);
            Assert.IsTrue(onlyTrue.Contains(1178));
            Assert.IsFalse(onlyTrue.Contains(1179));
            Assert.IsFalse(onlyTrue.Contains(1177));
        }
    }
}