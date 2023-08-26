using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TwoGuyGames.GTR.Core
{
    internal class RecordingControllerStaticTriggers
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CallBeginRecordingOrReplaying()
        {
        }

        /// <summary>
        /// This initializes recording and replaying before the scene is loaded.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void PatchBeforeSceneLoad()
        {
        }

        /// <summary>
        /// This triggers patching when the domain is being reloaded, but only if we are set to Recording or Replaying.
        /// That means this triggers only when we enter the playmode without having applied patches beforehand.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void PatchOnReload()
        {
        }
    }
}