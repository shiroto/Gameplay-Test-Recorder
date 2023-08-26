using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    internal class FieldMocker : IInputPatcher
    {
        private const string HARMONY_KEY = "FIELD MOCKER";
        private Harmony harmony;
        private List<TypeReweaveInfo> typesToReweave;

        public FieldMocker()
        {
            harmony = new Harmony(HARMONY_KEY);
        }

        public void Dispose()
        {
            harmony.UnpatchAll(HARMONY_KEY);
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
                MockFieldTranspiler.mockedFields = ttr.GetMockedFields();
                if (MockFieldTranspiler.mockedFields.Count > 0)
                {
                    //Debug.Log($"Patching Type=`{ttr.Target}`, Fields `{MockFieldTranspiler.mockedFields.Log()}`");
                    IReadOnlyList<MethodInfo> methods = ttr.GetPatchedMethods();
                    harmony.AddTranspilerToMethods(methods, MockFieldTranspiler.transpiler);
                }
            }
        }
    }
}