using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    internal class StaticMocker : IInputPatcher
    {
        private const string HARMONY_KEY = "STATIC MOCKER";
        private Harmony harmony;
        private List<TypeReweaveInfo> typesToReweave;

        public StaticMocker()
        {
            harmony = new Harmony(HARMONY_KEY);
        }

        public void Dispose()
        {
            harmony.UnpatchAll();
        }

        public void OnBeginRecording()
        {
        }

        public void OnBeginReplaying()
        {
        }

        public void OnEndRecording()
        {
        }

        public void OnEndReplaying()
        {
        }

        public void Patch(IInputPatchSettings_RO settings)
        {
            Assert.IsNotNull(settings);
            foreach (TypeToPatch ttr in settings.GetTypesToPatch())
            {
                StaticTranspiler.mockedTypes = ttr.GetStaticMockedTypes();
                if (StaticTranspiler.mockedTypes.Count > 0)
                {
                    //Debug.Log($"Patching Type=`{ttr.Target}`, Static Calls `{StaticTranspiler.mockedTypes.Log()}`");
                    IReadOnlyList<MethodInfo> methods = ttr.GetPatchedMethods();
                    harmony.AddTranspilerToMethods(methods, StaticTranspiler.transpiler);
                }
            }
        }
    }
}