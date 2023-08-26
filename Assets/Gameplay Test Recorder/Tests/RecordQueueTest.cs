using NUnit.Framework;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class RecordQueueTest
    {
        [Test]
        public void RecordQueueTest1()
        {
            RecordQueue q = new RecordQueue();
            Assert.AreEqual(0, q.Count);
            q.Enqueue(RecordFactory.CreateRecord(true));
            Assert.AreEqual(1, q.Count);
            q.Enqueue(RecordFactory.CreateRecord(false));
            Assert.AreEqual(2, q.Count);
            q.Enqueue(RecordFactory.CreateRecord(1));
            Assert.AreEqual(3, q.Count);
            q.Enqueue(RecordFactory.CreateRecord(1.3f));
            Assert.AreEqual(4, q.Count);
            q.Enqueue(RecordFactory.CreateRecord(1.5));
            Assert.AreEqual(5, q.Count);
            q.Enqueue(RecordFactory.CreateRecord("test"));
            Assert.AreEqual(6, q.Count);
            q.Enqueue(RecordFactory.CreateRecord('g'));
            Assert.AreEqual(7, q.Count);
            q.Enqueue(RecordFactory.CreateRecord(Vector2.one));
            Assert.AreEqual(8, q.Count);
            q.Enqueue(RecordFactory.CreateRecord(Vector3.one));
            Assert.AreEqual(9, q.Count);
            q.Enqueue(RecordFactory.CreateRecord(Vector4.one));
            Assert.AreEqual(10, q.Count);
            q.Enqueue(RecordFactory.CreateRecord(Quaternion.Euler(100, 100, 100)));
            Assert.AreEqual(11, q.Count);
            q.Enqueue(RecordFactory.CreateRecord(BindingFlags.Public));
            Assert.AreEqual(12, q.Count);

            Assert.AreEqual(true, q.Dequeue().Get);
            Assert.AreEqual(false, q.Dequeue().Get);
            Assert.AreEqual(1, q.Dequeue().Get);
            Assert.AreEqual(1.3f, q.Dequeue().Get);
            Assert.AreEqual(1.5, q.Dequeue().Get);
            Assert.AreEqual("test", q.Dequeue().Get);
            Assert.AreEqual('g', q.Dequeue().Get);
            Assert.AreEqual(Vector2.one, q.Dequeue().Get);
            Assert.AreEqual(Vector3.one, q.Dequeue().Get);
            Assert.AreEqual(Vector4.one, q.Dequeue().Get);
            Assert.AreEqual(Quaternion.Euler(100, 100, 100), q.Dequeue().Get);
            Assert.AreEqual(BindingFlags.Public, (BindingFlags)q.Dequeue().Get);
            Assert.AreEqual(0, q.Count);
        }
    }
}