using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class ByteRangeTests
    {
        [Test]
        public void TestsByte_Extend1()
        {
            IValueSpace<byte> range = RangeRecordFactory.CreateRange<byte>(0);
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
            IValueSpace<byte> range = RangeRecordFactory.CreateRange<byte>(68);
            range.Extend(10);
            Assert.IsTrue(range.Contains(10));
            Assert.IsTrue(range.Contains(68));
            Assert.IsTrue(range.Contains(34));
            Assert.IsFalse(range.Contains(9));
            Assert.IsFalse(range.Contains(68.4568));
        }

        [Test]
        public void TestsByte1()
        {
            IValueSpace<byte> range = RangeRecordFactory.CreateRange<byte>(0);
            Assert.IsTrue(range.Contains(0));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(1));
        }

        [Test]
        public void TestsByte2()
        {
            IValueSpace<byte> onlyTrue = RangeRecordFactory.CreateRange<byte>(178);
            Assert.IsTrue(onlyTrue.Contains(178));
            Assert.IsFalse(onlyTrue.Contains(179));
            Assert.IsFalse(onlyTrue.Contains(177));
        }
    }
}