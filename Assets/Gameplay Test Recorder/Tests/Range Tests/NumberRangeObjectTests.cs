using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    internal class NumberRangeObjectTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(16)]
        public void Test_Contains(double n)
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(n);
            Assert.IsTrue(range.Contains((object)(double)n));
            Assert.IsTrue(range.Contains((object)(float)n));
            Assert.IsTrue(range.Contains((object)(int)n));
            Assert.IsTrue(range.Contains((object)(uint)n));
            Assert.IsTrue(range.Contains((object)(short)n));
            Assert.IsTrue(range.Contains((object)(ushort)n));
            Assert.IsTrue(range.Contains((object)(byte)n));
            Assert.IsTrue(range.Contains((object)(sbyte)n));
            Assert.IsFalse(range.Contains((object)n.ToString()));
            Assert.IsFalse(range.Contains((object)true));
            Assert.IsFalse(range.Contains((object)false));
            Assert.IsFalse(range.Contains(new object()));
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(1, 10)]
        [TestCase(2, 55)]
        [TestCase(3, 77)]
        [TestCase(4, 13)]
        [TestCase(16, 101)]
        public void Test_Extend_Byte(byte value, byte extension)
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(value);
            range.Extend((object)extension);
            Assert.IsTrue(range.Contains((object)value));
            Assert.IsTrue(range.Contains((object)extension));
            Assert.IsTrue(range.Contains((object)((value + extension) / 2)));
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(1, 10)]
        [TestCase(2, 55)]
        [TestCase(3, 77)]
        [TestCase(4, 13)]
        [TestCase(16, 101)]
        public void Test_Extend_Double(double value, double extension)
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(value);
            range.Extend((object)extension);
            Assert.IsTrue(range.Contains((object)value));
            Assert.IsTrue(range.Contains((object)extension));
            Assert.IsTrue(range.Contains((object)((value + extension) / 2)));
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(1, 10)]
        [TestCase(2, 55)]
        [TestCase(3, 77)]
        [TestCase(4, 13)]
        [TestCase(16, 101)]
        public void Test_Extend_Float(float value, float extension)
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(value);
            range.Extend((object)extension);
            Assert.IsTrue(range.Contains((object)value));
            Assert.IsTrue(range.Contains((object)extension));
            Assert.IsTrue(range.Contains((object)((value + extension) / 2)));
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(1, 10)]
        [TestCase(2, 55)]
        [TestCase(3, 77)]
        [TestCase(4, 13)]
        [TestCase(16, 101)]
        public void Test_Extend_Int(int value, int extension)
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(value);
            range.Extend((object)extension);
            Assert.IsTrue(range.Contains((object)value));
            Assert.IsTrue(range.Contains((object)extension));
            Assert.IsTrue(range.Contains((object)((value + extension) / 2)));
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(1, 10)]
        [TestCase(2, 55)]
        [TestCase(3, 77)]
        [TestCase(4, 13)]
        [TestCase(16, 101)]
        public void Test_Extend_SByte(sbyte value, sbyte extension)
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(value);
            range.Extend((object)extension);
            Assert.IsTrue(range.Contains((object)value));
            Assert.IsTrue(range.Contains((object)extension));
            Assert.IsTrue(range.Contains((object)((value + extension) / 2)));
        }

        [Test]
        [TestCase(0, 5)]
        [TestCase(1, 10)]
        [TestCase(2, 55)]
        [TestCase(3, 77)]
        [TestCase(4, 13)]
        [TestCase(16, 101)]
        public void Test_Extend_Short(short value, short extension)
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(value);
            range.Extend((object)extension);
            Assert.IsTrue(range.Contains((object)value));
            Assert.IsTrue(range.Contains((object)extension));
            Assert.IsTrue(range.Contains((object)((value + extension) / 2)));
        }

        [Test]
        [TestCase((uint)0, (uint)5)]
        [TestCase((uint)1, (uint)10)]
        [TestCase((uint)2, (uint)55)]
        [TestCase((uint)3, (uint)77)]
        [TestCase((uint)4, (uint)13)]
        [TestCase((uint)16, (uint)101)]
        public void Test_Extend_UInt(uint value, uint extension)
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(value);
            range.Extend((object)extension);
            Assert.IsTrue(range.Contains((object)value));
            Assert.IsTrue(range.Contains((object)extension));
            Assert.IsTrue(range.Contains((object)((value + extension) / 2)));
        }

        [Test]
        [TestCase((ushort)0, (ushort)5)]
        [TestCase((ushort)1, (ushort)10)]
        [TestCase((ushort)2, (ushort)55)]
        [TestCase((ushort)3, (ushort)77)]
        [TestCase((ushort)4, (ushort)13)]
        [TestCase((ushort)16, (ushort)101)]
        public void Test_Extend_UShort(ushort value, ushort extension)
        {
            IValueSpace<double> range = RangeRecordFactory.CreateRange<double>(value);
            range.Extend((object)extension);
            Assert.IsTrue(range.Contains((object)value));
            Assert.IsTrue(range.Contains((object)extension));
            Assert.IsTrue(range.Contains((object)((value + extension) / 2)));
        }
    }
}