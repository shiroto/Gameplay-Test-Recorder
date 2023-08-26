using NUnit.Framework;
using System;
using System.Reflection;
using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    [TestFixture]
    public class MockPropertyTest2
    {
        private IInputPatcher reweaver;

        [Test]
        public void MockProperty3()
        {
            Recording recording = new Recording();
            reweaver = InputPatchFactory.CreateInstance(RecordedSystems.NONE);
            ReweaveSettingsMock settings = new ReweaveSettingsMock();
            settings.typeToReweave.Add(GetMock());
            reweaver.Patch(settings);

            RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.KEEP_RUNNING;

            TestClassWithProperties record = new TestClassWithProperties();
            record.myInt = 10;
            RecordingController.StartRecording(recording);
            int i = record.MyInt3;
            RecordingController.StopRecording();
            Assert.AreEqual(10, i);
            record = null;

            TestClassWithProperties replay = new TestClassWithProperties();
            replay.myInt = 5;
            RecordingController.StartReplaying(recording);
            int i2 = replay.MyInt3;
            RecordingController.StopReplaying();
            Assert.AreEqual(10, i2);
        }

        [SetUp]
        public void Setup()
        {
            RecordingController.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            reweaver.Dispose();
        }

        private static TypeToPatch GetMock()
        {
            Type type = typeof(TestClassWithProperties);
            Assert.IsNotNull(type);
            FieldInfo field1 = type.GetField("go");
            Assert.IsNotNull(field1);
            FieldInfo field2 = type.GetField("myInt");
            Assert.IsNotNull(field1);
            TypeToPatch mock = new TypeToPatch(type, new[] { field1, field2 });
            return mock;
        }
    }
}