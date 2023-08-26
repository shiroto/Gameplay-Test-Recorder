using NUnit.Framework;
using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core.Tests
{
    [TestFixture]
    public class StaticMockTest
    {
        private IInputPatcher reweaver;

        [SetUp]
        public void Setup()
        {
            RecordingController.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            reweaver?.Dispose();
        }

        [Test]
        public void Test_GameObject()
        {
            //Recording recording = new Recording();
            //reweaver = InputPatchFactory.CreateInstance(RecordedSystems.NONE);
            //ReweaveSettingsMock settings = new ReweaveSettingsMock();
            //settings.typeToReweave.Add(GetMockForTestClass());
            //reweaver.Patch(settings);

            //RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.KEEP_RUNNING;

            //StaticMockTestClass record = new StaticMockTestClass();
            //StaticClass.gameObject = new GameObject("obj");
            //RecordingController.StartRecording(recording);
            //record.ReadGameObject();
            //RecordingController.StopRecording();
            //Assert.AreEqual("obj", record.gameObject.name);

            //StaticMockTestClass replay = new StaticMockTestClass();
            //StaticClass.gameObject = null;
            //RecordingController.StartReplaying(recording);
            //replay.ReadGameObject();
            //RecordingController.StopReplaying();
            //Assert.AreEqual("obj", replay.gameObject.name);
        }

        [Test]
        public void Test_Int()
        {
            Recording recording = new Recording();
            reweaver = InputPatchFactory.CreateInstance(RecordedSystems.NONE);
            ReweaveSettingsMock settings = new ReweaveSettingsMock();
            settings.typeToReweave.Add(GetMockForTestClass());
            reweaver.Patch(settings);

            RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.KEEP_RUNNING;

            StaticMockTestClass record = new StaticMockTestClass();
            StaticClass.intValue = 10;
            RecordingController.StartRecording(recording);
            record.ReadStaticClass();
            RecordingController.StopRecording();
            Assert.AreEqual(10, record.result);

            StaticMockTestClass replay = new StaticMockTestClass();
            StaticClass.intValue = 5;
            RecordingController.StartReplaying(recording);
            replay.ReadStaticClass();
            RecordingController.StopReplaying();
            Assert.AreEqual(10, replay.result);
        }

        private static TypeToPatch GetMockForTestClass()
        {
            Type type = typeof(StaticMockTestClass);
            TypeToPatch mock = new TypeToPatch(type, typeof(StaticClass));
            return mock;
        }
    }
}