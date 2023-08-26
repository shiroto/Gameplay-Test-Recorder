using NUnit.Framework;
using System.Collections.Generic;

namespace TwoGuyGames.GTR.Core.Tests
{
    public class InputRecordingGlobalSettingsTests
    {
        [Test]
        public void TestBeforeSerialize()
        {
            InputRecordingGlobalSettings settings = new InputRecordingGlobalSettings();
            TypeToPatch ttr = new TypeToPatch(typeof(object), RecordedSystems.REWIRED);
            settings.AddTypeToReweave(ttr);
            settings.AddTypeToReweave(ttr);
            settings.MergeDuplicateTypes();
            Assert.AreEqual(1, settings.GetTypesToPatch().Count);
        }

        [Test]
        public void TestTypesToReweave()
        {
            InputRecordingGlobalSettings settings = new InputRecordingGlobalSettings();
            settings.AddTypeToReweave(new TypeToPatch(typeof(object), RecordedSystems.REWIRED));
            settings.AddTypeToReweave(new TypeToPatch(typeof(string), RecordedSystems.UNITY_INPUT_SYSTEM));
            Assert.AreEqual(RecordedSystems.REWIRED | RecordedSystems.UNITY_INPUT_SYSTEM, settings.GetInputSolutions());
            IReadOnlyList<TypeToPatch> list = settings.GetTypesToPatch();
            Assert.AreEqual(typeof(object), list[0].Target);
            Assert.AreEqual(RecordedSystems.REWIRED, list[0].RecordedSystems);
            Assert.AreEqual(typeof(string), list[1].Target);
            Assert.AreEqual(RecordedSystems.UNITY_INPUT_SYSTEM, list[1].RecordedSystems);
            settings.Clear();
            Assert.AreEqual(0, settings.GetTypesToPatch().Count);
        }
    }
}