using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    public class InputRecordingGlobalSettingsAsset : ScriptableObject, ISerializationCallbackReceiver
    {
        public InputRecordingGlobalSettings settings;

        public static event Action OnUsedInputChanged = delegate { };

        public void Clear()
        {
#if UNITY_EDITOR
            settings.Clear();
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        public bool IsReady()
        {
            if (settings == null)
            {
                return false;
            }
            else
            {
                return settings.IsReady();
            }
        }

        public void OnAfterDeserialize()
        {
            TryMergeDuplicates();
        }

        public void OnBeforeSerialize()
        {
            TryMergeDuplicates();
        }

        public void ToggleUsedInput(RecordedSystems input)
        {
            if (settings != null)
            {
                settings.UsedInput ^= input;
                OnUsedInputChanged();
            }
        }

        public bool UsesInput(RecordedSystems input)
        {
            if (settings != null)
            {
                return settings.UsedInput.HasFlag(input);
            }
            else
            {
                return false;
            }
        }

        private void TryMergeDuplicates()
        {
            try
            {
                settings?.MergeDuplicateTypes();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError("Cannot load global settings, please re-analyze!");
            }
        }
    }
}