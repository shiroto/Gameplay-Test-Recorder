using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class CharRangeTests
    {
        [Test]
        public void Test_1()
        {
            IValueSpace<char> range = RangeRecordFactory.CreateRange<char>('a');
            Assert.IsTrue(range.Contains('a'));
            Assert.IsFalse(range.Contains('b'));
            Assert.IsFalse(range.Contains((int)'a'));
        }

        [Test]
        public void Test_2()
        {
            IValueSpace<char> range = RangeRecordFactory.CreateRange<char>('a');
            range.Extend('c');
            Assert.IsTrue(range.Contains('a'));
            Assert.IsTrue(range.Contains('c'));
            Assert.IsFalse(range.Contains('b'));
            Assert.IsFalse(range.Contains('d'));
        }
    }
}