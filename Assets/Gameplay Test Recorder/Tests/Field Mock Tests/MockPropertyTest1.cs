using NUnit.Framework;
using System;
using System.Reflection;

namespace TwoGuyGames.GTR.Core.Tests
{
    [TestFixture]
    public class MockPropertyTest1
    {
        private IInputPatcher reweaver;

        [Test]
        public void MockProperty1()
        {
            Recording recording = new Recording();
            reweaver = InputPatchFactory.CreateInstance(RecordedSystems.NONE);
            ReweaveSettingsMock settings = new ReweaveSettingsMock();
            settings.typeToReweave.Add(GetMock());
            reweaver.Patch(settings);

            RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.KEEP_RUNNING;

            TestClassWithIntProperty record = new TestClassWithIntProperty();
            record.myInt = 10;
            RecordingController.StartRecording(recording);
            Assert.IsTrue(RecordingController.IsRecording);
            int i = record.MyInt;
            RecordingController.StopRecording();
            Assert.AreEqual(10, i);
            record = null;

            TestClassWithIntProperty replay = new TestClassWithIntProperty();
            replay.myInt = 5;
            RecordingController.StartReplaying(recording);
            Assert.IsTrue(RecordingController.IsReplaying);
            var res = ValueRecorder.NextInput<int>("TestClassWithIntProperty.get_MyInt");
            UnityEngine.Debug.Log(res);
            Assert.AreEqual(10, res);
            //int i2 = replay.MyInt;
            RecordingController.StopReplaying();
            //Assert.AreEqual(10, i2);
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
            Type type = typeof(TestClassWithIntProperty);
            Assert.IsNotNull(type);
            FieldInfo field1 = type.GetField("myInt");
            Assert.IsNotNull(field1);
            TypeToPatch mock = new TypeToPatch(type, new[] { field1 });
            return mock;
        }
    }
}