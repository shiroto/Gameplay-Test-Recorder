using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class FloatRangeTests
    {
        [Test]
        public void TestsFloat_Extend1()
        {
            IValueSpace<float> range = RangeRecordFactory.CreateRange<float>(0);
            range.Extend(10);
            Assert.IsTrue(range.Contains(0));
            Assert.IsTrue(range.Contains(5));
            Assert.IsTrue(range.Contains(10));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(11));
        }

        [Test]
        public void TestsFloat_Extend2()
        {
            IValueSpace<float> range = RangeRecordFactory.CreateRange<float>(68.4567f);
            range.Extend(10);
            Assert.IsTrue(range.Contains(10));
            Assert.IsTrue(range.Contains(68.4567));
            Assert.IsTrue(range.Contains(68.56 / 2.0765));
            Assert.IsFalse(range.Contains(9));
            Assert.IsFalse(range.Contains(68.4568));
        }

        [Test]
        public void TestsFloat1()
        {
            IValueSpace<float> range = RangeRecordFactory.CreateRange<float>(0);
            Assert.IsTrue(range.Contains(0));
            Assert.IsFalse(range.Contains(-1));
            Assert.IsFalse(range.Contains(1));
        }

        [Test]
        public void TestsFloat2()
        {
            IValueSpace<float> onlyTrue = RangeRecordFactory.CreateRange<float>(8652.56f);
            Assert.IsTrue(onlyTrue.Contains(8652.56f));
            Assert.IsFalse(onlyTrue.Contains(8652.55f));
            Assert.IsFalse(onlyTrue.Contains(8652.57f));
        }
    }
}