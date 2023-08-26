using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    public static class Zip
    {
        #region SAMPLES

        private const string INPUT_MANAGER_SAMPLES_ZIP = "/Gameplay Test Recorder/Extra/Input Manager Samples.zip";
        private const string INPUT_SYSTEM_SAMPLES_ZIP = "/Gameplay Test Recorder/Extra/Input System Samples.zip";
        private const string REWIRED_SAMPLES_ZIP = "/Gameplay Test Recorder/Extra/Rewired Samples.zip";

        #endregion SAMPLES

        #region ADAPTERS

        private const string INPUT_MANAGER_ADAPTER_ZIP = "/Gameplay Test Recorder/Extra/TwoGuyGames.GTR.InputManagerAdapter.zip";
        private const string INPUT_SYSTEM_ADAPTER_ZIP = "/Gameplay Test Recorder/Extra/TwoGuyGames.GTR.InputSystemAdapter.zip";
        private const string REWIRED_ADAPTER_ZIP = "/Gameplay Test Recorder/Extra/TwoGuyGames.GTR.RewiredAdapter.zip";

        #endregion ADAPTERS

        private const string ADAPTER_TARGET = "/Gameplay Test Recorder/Runtime/Adapters/";

        private const string SAMPLES_TARGET = "/Gameplay Test Recorder/Samples/";

        public static void UnpackInputManager()
        {
            string adapterZip = GetAbsolutePath(INPUT_MANAGER_ADAPTER_ZIP);
            string samplesZip = GetAbsolutePath(INPUT_MANAGER_SAMPLES_ZIP);
            DecompressZip(adapterZip, GetAbsolutePath(ADAPTER_TARGET));
            DecompressZip(samplesZip, GetAbsolutePath(SAMPLES_TARGET));
        }

        public static void UnpackInputSystem()
        {
            string adapterZip = GetAbsolutePath(INPUT_SYSTEM_ADAPTER_ZIP);
            string samplesZip = GetAbsolutePath(INPUT_SYSTEM_SAMPLES_ZIP);
            DecompressZip(adapterZip, GetAbsolutePath(ADAPTER_TARGET));
            DecompressZip(samplesZip, GetAbsolutePath(SAMPLES_TARGET));
        }

        public static void UnpackRewired()
        {
            string adapterZip = GetAbsolutePath(REWIRED_ADAPTER_ZIP);
            string samplesZip = GetAbsolutePath(REWIRED_SAMPLES_ZIP);
            DecompressZip(adapterZip, GetAbsolutePath(ADAPTER_TARGET));
            DecompressZip(samplesZip, GetAbsolutePath(SAMPLES_TARGET));
        }

        private static void Decompress(string path, string target)
        {
            ZipStorer zip = ZipStorer.Open(path, FileAccess.Read);
            List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();
            foreach (ZipStorer.ZipFileEntry entry in dir)
            {
                //Debug.Log(entry.ToString());
                zip.ExtractFile(entry, target + entry.ToString());
            }
            zip.Close();
        }

        private static void DecompressZip(string zip, string targetPath)
        {
            if (!File.Exists(zip))
            {
                Debug.LogError("Can't unpack files, because files don't exist!");
                return;
            }
            Decompress(zip, targetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static string GetAbsolutePath(string relativePath)
        {
            return Application.dataPath + relativePath;
        }
    }
}