using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class BoolRangeTests
    {
        [Test]
        public void TestBoolRangeBoth()
        {
            IValueSpace<bool> onlyTrue = RangeRecordFactory.CreateRange<bool>(true);
            onlyTrue.Extend(false);
            Assert.IsTrue(onlyTrue.Contains(false));
            Assert.IsTrue(onlyTrue.Contains(true));
        }

        [Test]
        public void TestBoolRangeFalse()
        {
            IValueSpace<bool> onlyFalse = RangeRecordFactory.CreateRange<bool>(false);
            Assert.IsTrue(onlyFalse.Contains(false));
            Assert.IsFalse(onlyFalse.Contains(true));
        }

        [Test]
        public void TestBoolRangeTrue()
        {
            IValueSpace<bool> onlyTrue = RangeRecordFactory.CreateRange<bool>(true);
            Assert.IsFalse(onlyTrue.Contains(false));
            Assert.IsTrue(onlyTrue.Contains(true));
        }
    }
}