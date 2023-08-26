using NUnit.Framework;
using System;
using System.Reflection;
using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    [TestFixture]
    public class CombiMockTest
    {
        private IInputPatcher reweaver;

        [TearDown]
        public void TearDown()
        {
            reweaver.Dispose();
        }

        [Test]
        public void Test1()
        {
            Recording recording = new Recording();
            reweaver = InputPatchFactory.CreateInstance(RecordedSystems.NONE);
            ReweaveSettingsMock settings = new ReweaveSettingsMock();
            settings.typeToReweave.Add(GetMockForTestClass());
            reweaver.Patch(settings);

            RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.KEEP_RUNNING;

            CombiTestClass record = new CombiTestClass();
            StaticClass.intValue = 10;
            record.mockInt = 11;
            RecordingController.StartRecording(recording);
            record.GetInts();
            RecordingController.StopRecording();
            Assert.AreEqual(10, record.int1);
            Assert.AreEqual(11, record.int2);

            CombiTestClass replay = new CombiTestClass();
            StaticClass.intValue = 0;
            replay.mockInt = 0;
            RecordingController.StartReplaying(recording);
            replay.GetInts();
            RecordingController.StopReplaying();
            Assert.AreEqual(10, replay.int1);
            Assert.AreEqual(11, replay.int2);
        }

        [Test]
        public void Test2_SeparateTTR()
        {
            Recording recording = new Recording();
            reweaver = InputPatchFactory.CreateInstance(RecordedSystems.NONE);
            ReweaveSettingsMock settings = new ReweaveSettingsMock();
            settings.typeToReweave.AddRange(GetMockForTestClass_Separate());
            reweaver.Patch(settings);

            RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.KEEP_RUNNING;

            CombiTestClass record = new CombiTestClass();
            StaticClass.intValue = 10;
            record.mockInt = 11;
            RecordingController.StartRecording(recording);
            record.GetInts();
            RecordingController.StopRecording();
            Assert.AreEqual(10, record.int1);
            Assert.AreEqual(11, record.int2);

            CombiTestClass replay = new CombiTestClass();
            StaticClass.intValue = 0;
            replay.mockInt = 0;
            RecordingController.StartReplaying(recording);
            replay.GetInts();
            RecordingController.StopReplaying();
            Assert.AreEqual(10, replay.int1);
            Assert.AreEqual(11, replay.int2);
        }

        private static TypeToPatch GetMockForTestClass()
        {
            Type type = typeof(CombiTestClass);
            var fields = new FieldInfo[] { type.GetField("mockInt") };
            var statics = new StaticMock[] { new StaticMock(typeof(StaticClass)) };
            TypeToPatch mock = new TypeToPatch(type, fields, statics);
            return mock;
        }

        private static TypeToPatch[] GetMockForTestClass_Separate()
        {
            Type type = typeof(CombiTestClass);
            var fields = new FieldInfo[] { type.GetField("mockInt") };
            var statics = new Type[] { typeof(StaticClass) };
            TypeToPatch mock1 = new TypeToPatch(type, fields);
            TypeToPatch mock2 = new TypeToPatch(type, statics);
            return new[] { mock1, mock2 };
        }
    }
}