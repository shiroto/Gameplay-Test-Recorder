using NUnit.Framework;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace TwoGuyGames.GTR.Core.Tests
{
    [TestFixture]
    public class MockFieldTests
    {
        private IInputPatcher reweaver;

        [Test]
        public void MockField_TestClass1_Method1()
        {
            Recording recording = new Recording();
            reweaver = InputPatchFactory.CreateInstance(RecordedSystems.NONE);
            ReweaveSettingsMock settings = new ReweaveSettingsMock();
            settings.typeToReweave.Add(GetMockForTestClass1());
            reweaver.Patch(settings);

            RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.KEEP_RUNNING;

            TestClassWithFields record = new TestClassWithFields();
            record.classField.intValue = 10;
            RecordingController.StartRecording(recording);
            record.Method1();
            RecordingController.StopRecording();
            Assert.AreEqual(10, record.result);
            record = null;

            TestClassWithFields replay = new TestClassWithFields();
            replay.classField.intValue = 5;
            RecordingController.StartReplaying(recording);
            replay.Method1();
            RecordingController.StopReplaying();
            Assert.AreEqual(10, replay.result);
        }

        [Test]
        public void MockField_TestClass1_Method2()
        {
            Recording recording = new Recording();
            reweaver = InputPatchFactory.CreateInstance(RecordedSystems.NONE);
            ReweaveSettingsMock settings = new ReweaveSettingsMock();
            settings.typeToReweave.Add(GetMockForTestClass1());
            reweaver.Patch(settings);

            RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.KEEP_RUNNING;

            TestClassWithFields record = new TestClassWithFields();
            record.classField.floatValue = 2.5f;
            RecordingController.StartRecording(recording);
            record.Method2();
            RecordingController.StopRecording();
            Assert.AreEqual(2.5f, record.result);
            record = null;

            TestClassWithFields replay = new TestClassWithFields();
            replay.classField.floatValue = 7.8f;
            RecordingController.StartReplaying(recording);
            replay.Method2();
            RecordingController.StopReplaying();
            Assert.AreEqual(2.5f, replay.result);
        }

        [Test]
        public void MockField_TestClass1_Method3()
        {
            Recording recording = new Recording();
            reweaver = InputPatchFactory.CreateInstance(RecordedSystems.NONE);
            ReweaveSettingsMock settings = new ReweaveSettingsMock();
            settings.typeToReweave.Add(GetMockForTestClass1());
            reweaver.Patch(settings);

            RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.KEEP_RUNNING;

            TestClassWithFields record = new TestClassWithFields();
            RecordingController.StartRecording(recording);
            record.myInt = 5;
            record.Method3();
            RecordingController.StopRecording();
            Assert.AreEqual(5f, record.result);
            record = null;

            TestClassWithFields replay = new TestClassWithFields();
            RecordingController.StartReplaying(recording);
            replay.myInt = 10;
            replay.Method3();
            RecordingController.StopReplaying();
            Assert.AreEqual(5f, replay.result);
        }

        [Test]
        public void MockInputField()
        {
            // Just check if this produces valid IL code.
            reweaver = InputPatchFactory.CreateInstance(RecordedSystems.NONE);
            ReweaveSettingsMock settings = new ReweaveSettingsMock();
            settings.typeToReweave.Add(GetMockForInputField());
            reweaver.Patch(settings);
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

        private static TypeToPatch GetMockForInputField()
        {
            Type t = typeof(InputField);
            Assert.IsNotNull(t);
            FieldInfo field = t.GetField("m_ProcessingEvent", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(field);
            TypeToPatch mock = new TypeToPatch(t, new[] { field }, new[] { new StaticMock(typeof(Event)) });
            return mock;
        }

        private static TypeToPatch GetMockForTestClass1()
        {
            Type type = typeof(TestClassWithFields);
            Assert.IsNotNull(type);
            FieldInfo field1 = type.GetField("classField");
            Assert.IsNotNull(field1);
            FieldInfo field2 = type.GetField("myInt");
            Assert.IsNotNull(field2);
            TypeToPatch mock = new TypeToPatch(type, new FieldInfo[] { field1, field2 });
            return mock;
        }
    }
}