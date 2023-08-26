using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.Assertions;

namespace TwoGuyGames.GTR.Core
{
    public abstract class PatcherBase : IInputPatcher
    {
        private static readonly Type[] ALL_TYPES;
        private Harmony harmony;
        private string key;
        private List<Type> typesToReweave;

        static PatcherBase()
        {
            ALL_TYPES = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).ToArray();
        }

        public PatcherBase(string key)
        {
            this.key = key;
            harmony = new Harmony(key);
            typesToReweave = new List<Type>();
        }

        protected abstract RecordedSystems SupportedSolution
        {
            get;
        }

        public virtual void Dispose()
        {
            harmony.UnpatchAll();
        }

        public virtual void OnBeginRecording()
        {
        }

        public virtual void OnBeginReplaying()
        {
        }

        public virtual void OnEndRecording()
        {
        }

        public virtual void OnEndReplaying()
        {
        }

        public abstract void Patch(IInputPatchSettings_RO settings);

        protected void AddTranspilerToTypes(IInputPatchSettings_RO settings, MethodInfo transpiler)
        {
            CreateTypeList(settings);
            PatchType(transpiler);
        }

        private static Type[] GetAllAssignablesClasses(Type original)
        {
            Assert.IsNotNull(original);
            List<Type> types = new List<Type>(16);
            types.Add(original);
            types.AddRange(ALL_TYPES.AsParallel().Where(t => t.IsSubclassOf(original)));
            return types.ToArray();
        }

        private void CreateTypeList(IInputPatchSettings_RO settings)
        {
            typesToReweave = settings.GetTypesToPatch().AsParallel()
                 .Where(ttp => ttp.RecordedSystems.HasFlag(SupportedSolution))
                 .SelectMany(ttr => GetAllAssignablesClasses(ttr.Target))
                 .Distinct().ToList();
        }

        private void PatchType(MethodInfo transpiler)
        {
            for (int i = 0; i < typesToReweave.Count; i++)
            {
                MethodInfo[] methods = ReflectionHelper.FindDeclaredNonGenericMethods(typesToReweave[i]);
                harmony.AddTranspilerToMethods(methods, transpiler);
            }
        }
    }
}