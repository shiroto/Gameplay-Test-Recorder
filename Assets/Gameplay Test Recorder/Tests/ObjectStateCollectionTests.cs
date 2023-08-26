using NUnit.Framework;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class ObjectStateCollectionTests
    {
        [Test]
        public void ClearTest()
        {
            ObjectStateHashset states = new ObjectStateHashset("id");
            states.Add(new RecordState("1", RecordFactory.CreateRecord(true)));
            Assert.AreEqual(1, states.Count);
            states.Add(new RecordState("2", RecordFactory.CreateRecord(true)));
            Assert.AreEqual(2, states.Count);
            states.Clear();
            Assert.AreEqual(0, states.Count);
        }

        [Test]
        public void EmptyTests()
        {
            ObjectStateHashset states = new ObjectStateHashset("id");
            Assert.AreEqual(0, states.Count);
            states.Clear();
            Assert.AreEqual(0, states.Count);
            Assert.AreEqual("id", states.Id);
            Assert.IsFalse(states.IsReadOnly);
            Assert.IsTrue(states.StateMatches(new ObjectStateHashset("id")));
        }

        [Test]
        public void MatchesTest()
        {
            ObjectStateHashset states = new ObjectStateHashset("id");
            states.Add(new RecordState("1", RecordFactory.CreateRecord(true)));
            states.Add(new RecordState("2", RecordFactory.CreateRecord(true)));

            ObjectStateHashset states2 = new ObjectStateHashset("id");
            states2.Add(new RecordState("1", RecordFactory.CreateRecord(true)));
            states2.Add(new RecordState("2", RecordFactory.CreateRecord(true)));

            Assert.IsTrue(states.StateMatches(states2));
        }
    }
}