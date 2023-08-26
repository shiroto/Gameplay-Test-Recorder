using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine.TestTools;
using TwoGuyGames.GTR.Core;
using TwoGuyGames.GTR.Editor;

namespace TwoGuyGames.GTR.Replays
{
    [TestFixture]
    internal class Test_UGUI_Input_System_Replay
    {
        private const string ASSET_GUID = "24342df44140f054693529ffa1410675";

        [TearDown]
        public void Cleanup()
        {
            RecordingController.StopReplaying();
        }

        [UnityTest]
        public IEnumerator Run_UGUI_Input_System_Replay()
        {
             // Check if this replay should be run.
            if (!ArgHelper.IsReplayEnabled())
            {
                Assert.Pass("Skipping replays due to command line argument `noReplays`.");
            }
            string path = AssetDatabase.GUIDToAssetPath(ASSET_GUID);
            RecordedTestAsset testAsset = AssetDatabase.LoadAssetAtPath<RecordedTestAsset>(path);
            if (testAsset == null)
            {
                Assert.Fail("Test asset not found");
            }
            if (!testAsset.IsValid())
            {
                Assert.Fail($"Test asset is invalid");
            }
            if (!IsInputEnabled(testAsset))
            {
                yield break;
            }
            yield return ExecuteTest(testAsset);
        }

        private static IEnumerator ExecuteTest(RecordedTestAsset testAsset)
        {
            yield return RunReplay(testAsset);
            if (RecordingController.LastResult > 10)
            {
                // Rerun since tests can be a bit flakey still.
                yield return RunReplay(testAsset);
            }
            if (RecordingController.LastResult > 10)
            {
                Assert.Fail("Failed with diff: " + RecordingController.LastResult);
            }
            else
            {
                Assert.Pass();
            }
        }

        private static bool IsInputEnabled(RecordedTestAsset testAsset)
        {
            bool res = true;
            if (RecordingInputTypeHelper.ContainsInputSystem(testAsset))
            {
                if (!ActiveInputUtility.IsInputSystemEnabled())
                {
                    Assert.Pass("Input System is not enabled, skipping.");
                    res &= false;
                }
                if (ArgHelper.IsBatchmode())
                {
                    Assert.Pass("The new Unity Input System is currently not supported in batchmode.");
                    res &= false;
                }
            }
            if (RecordingInputTypeHelper.ContainsLegacyInput(testAsset))
            {
                if (!ActiveInputUtility.IsLegacyInputEnabled())
                {
                    Assert.Pass("Legacy input is not enabled, skipping.");
                    res &= false;
                }
            }
            if (RecordingInputTypeHelper.ContainsRewired(testAsset))
            {
                if (!ActiveInputUtility.IsRewiredEnabled())
                {
                    Assert.Pass("Rewired is not enabled, skipping.");
                    res &= false;
                }
            }
            return true;
        }

        private static IEnumerator RunReplay(RecordedTestAsset testAsset)
        {
            RecordingController.ReplayFinishedBehaviour = ReplayFinishedMode.EXIT_PLAY_MODE;
            RecordingController.ApplyPatches();
            RecordingController.SetupAndStartReplaying(testAsset.recording);
            while (RecordingController.IsReplaying)
            {
                yield return null;
            }
        }
    }
}