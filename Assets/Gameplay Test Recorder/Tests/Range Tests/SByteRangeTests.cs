using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class SByteRangeTests
    {
        [Test]
        public void TestsByte_Extend1()
        {
            IValueSpace<sbyte> range = RangeRecordFactory.CreateRange<sbyte>(0);
            range.Extend(10);
            Assert.IsTrue(range.Contains(0));
            Assert.IsTrue(range.Contains(5));
            Assert.IsTrue(range.Contains(10));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(11));
        }

        [Test]
        public void TestsByte_Extend2()
        {
            IValueSpace<sbyte> range = RangeRecordFactory.CreateRange<sbyte>(-68);
            range.Extend(10);
            Assert.IsTrue(range.Contains(10));
            Assert.IsTrue(range.Contains(-68));
            Assert.IsTrue(range.Contains(-20));
            Assert.IsFalse(range.Contains(11));
            Assert.IsFalse(range.Contains(-69));
        }

        [Test]
        public void TestsByte1()
        {
            IValueSpace<sbyte> range = RangeRecordFactory.CreateRange<sbyte>(0);
            Assert.IsTrue(range.Contains(0));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(1));
        }

        [Test]
        public void TestsByte2()
        {
            IValueSpace<sbyte> onlyTrue = RangeRecordFactory.CreateRange<sbyte>(55);
            Assert.IsTrue(onlyTrue.Contains(55));
            Assert.IsFalse(onlyTrue.Contains(56));
            Assert.IsFalse(onlyTrue.Contains(54));
        }
    }
}