using System.IO;
using UnityEditor;

namespace TwoGuyGames.GTR.Core
{
    public static class TestRunnerHelper
    {
        private const string ASSEMBLY_TEMPLATE_GUID = "9866977982c4eb24482260e117e242bd";
#if BUILD_AS_PACKAGE
        private const string TARGET_DIR = "Assets/2GuyGames/Replay Tests";
#else
        private const string TARGET_DIR = "Assets/Gameplay Test Recorder/Replay Tests";
#endif
        private const string TEST_TEMPLATE_GUID = "8d05b8275ad63ff4c8026a5b86293f0c";

        public static void RefreshReplayTestAssembly()
        {
            ResetTargetDir();
            CreateAssemblyIfNeeded();
            CreateTestCases();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        public static bool TestAssemblyExists()
        {
            return AssetDatabase.IsValidFolder(TARGET_DIR);
        }

        private static void CreateAssemblyIfNeeded()
        {
            string targetPath = Path.Combine(TARGET_DIR, "TestRunnerIntegration.asmdef");
            if (!File.Exists(targetPath))
            {
                string templatePath = AssetDatabase.GUIDToAssetPath(ASSEMBLY_TEMPLATE_GUID);
                string content = File.ReadAllText(templatePath);
                File.WriteAllText(targetPath, content);
            }
        }

        private static void CreateTestCases()
        {
            string templatePath = AssetDatabase.GUIDToAssetPath(TEST_TEMPLATE_GUID);
            string template = File.ReadAllText(templatePath);
            string[] assets = AssetDatabase.FindAssets("t:RecordedTestAsset");
            foreach (string guid in assets)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                RecordedTestAsset asset = AssetDatabase.LoadAssetAtPath<RecordedTestAsset>(path);
                if (asset.IsValid())
                {
                    string name = asset.name.Replace(' ', '_');
                    string testCs = template.Replace("SET_GUID_HERE", guid).Replace("SET_NAME_HERE", name);
                    string testCsPath = Path.Combine(TARGET_DIR, name + ".cs");
                    File.WriteAllText(testCsPath, testCs);
                }
            }
        }

        private static void ResetTargetDir()
        {
            if (Directory.Exists(TARGET_DIR))
            {
                string[] files = Directory.GetFiles(TARGET_DIR, "*.cs");
                foreach (string f in files)
                {
                    File.Delete(f);
                }
            }
            else
            {
                Directory.CreateDirectory(TARGET_DIR);
            }
        }
    }
}