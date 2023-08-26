using NUnit.Framework;
using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class ValueSpaceCollectionTests
    {
        [Test]
        public void Test_1()
        {
            IObjectState state = new RecordState("id", RecordFactory.CreateRecord(10));
            IValueSpace range = StateToRange.ConvertToRange(state);
            range.Extend(50);

            Assert.IsTrue(range.Contains(10));
            Assert.IsTrue(range.Contains(33));
            Assert.IsTrue(range.Contains(50));
            Assert.IsTrue(range.Contains(25.555));
        }

        [Test]
        public void Test_2()
        {
            IValueSpaceCollection collection = new ValueSpaceCollection("hs");
            collection.Add(RangeRecordFactory.CreateRange("id", 10));
            collection.Add(RangeRecordFactory.CreateRange("id2", new Vector2(10, 0)));

            ObjectStateHashset extension = new ObjectStateHashset("hs");
            extension.Add(new RecordState("id", RecordFactory.CreateRecord(50)));
            extension.Add(new RecordState("id2", RecordFactory.CreateRecord(new Vector2(12, 0))));
            collection.Extend(extension);

            ObjectStateHashset test = new ObjectStateHashset("hs");
            extension.Add(new RecordState("id", RecordFactory.CreateRecord(10)));
            Assert.IsTrue(collection.Contains(test));

            test = new ObjectStateHashset("hs");
            extension.Add(new RecordState("id", RecordFactory.CreateRecord(33)));
            Assert.IsTrue(collection.Contains(test));

            test = new ObjectStateHashset("hs");
            extension.Add(new RecordState("id", RecordFactory.CreateRecord(50)));
            Assert.IsTrue(collection.Contains(test));

            test = new ObjectStateHashset("hs");
            extension.Add(new RecordState("id", RecordFactory.CreateRecord(25.555)));
            Assert.IsTrue(collection.Contains(test));

            test = new ObjectStateHashset("hs");
            extension.Add(new RecordState("id2", RecordFactory.CreateRecord(new Vector2(10, 2))));
            Assert.IsTrue(collection.Contains(test));

            test = new ObjectStateHashset("hs");
            extension.Add(new RecordState("id2", RecordFactory.CreateRecord(new Vector2(10, 1))));
            Assert.IsTrue(collection.Contains(test));

            test = new ObjectStateHashset("hs");
            extension.Add(new RecordState("id2", RecordFactory.CreateRecord(new Vector2(12, 0))));
            Assert.IsTrue(collection.Contains(test));

            test = new ObjectStateHashset("hs");
            extension.Add(new RecordState("id2", RecordFactory.CreateRecord(new Vector2(10, 0))));
            Assert.IsTrue(collection.Contains(test));
        }
    }
}