using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class StringRangeTests
    {
        [Test]
        public void TestString1()
        {
            IValueSpace<string> range = RangeRecordFactory.CreateRange<string>("test");
            Assert.IsTrue(range.Contains("test"));
            Assert.IsFalse(range.Contains("test1"));
            Assert.IsFalse(range.Contains("tes"));
        }

        [Test]
        public void TestString2()
        {
            IValueSpace<string> range = RangeRecordFactory.CreateRange<string>("test");
            range.Extend("test2");
            Assert.IsTrue(range.Contains("test"));
            Assert.IsTrue(range.Contains("test2"));
            Assert.IsFalse(range.Contains("test1"));
            Assert.IsFalse(range.Contains("tes"));
        }
    }
}