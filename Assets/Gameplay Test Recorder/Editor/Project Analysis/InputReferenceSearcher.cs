using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using TwoGuyGames.GTR.Core;

namespace TwoGuyGames.GTR.Editor
{
    internal static class InputReferenceSearcher
    {
        public static void AnalyzeProject(InputRecordingGlobalSettingsAsset settingsAsset)
        {
            IReadOnlyCollection<IRecordedType> list = TypeRecordSettings.GetRecordedTypes();
            if (list.Count != 0)
            {
                TryAnalyzeProject(settingsAsset, list);
            }
        }

        private static void AddPredefinedMocks(List<TypeToPatch> typesToReweave, IEnumerable<TypeToPatch> add)
        {
            foreach (TypeToPatch ttr in add)
            {
                TypeToPatch existing = typesToReweave.FirstOrDefault(ttr2 => ttr2.Target.Equals(ttr.Target));
                if (existing != null)
                {
                    typesToReweave.Remove(existing);
                    typesToReweave.Add(TypeToPatch.Merge(ttr, existing));
                }
                else
                {
                    typesToReweave.Add(ttr);
                }
            }
        }

        private static void AddPredefinedMocks(List<TypeToPatch> typesToReweave)
        {
            AddPredefinedMocks(typesToReweave, UnityGuiHelper.TypesToPatch);
            AddPredefinedMocks(typesToReweave, TMProHelper.TypesToPatch);
        }

        private static void FindReferencesForRecordedTypes(InputRecordingGlobalSettings settings, Assembly[] assemblies, IReadOnlyCollection<IRecordedType> recordedTypes)
        {
            List<TypeToPatch> types = new List<TypeToPatch>();
            FindTypesToReweave(assemblies, recordedTypes, types);
            types = types.Where(t => t.RecordedSystems != RecordedSystems.NONE).ToList();
            AddPredefinedMocks(types);
            foreach (TypeToPatch type in types)
            {
                Debug.Log($"Found input type `{type}`");
                settings.AddTypeToReweave(type);
            }
        }

        private static void FindTypesToReweave(Assembly[] assemblies, IReadOnlyCollection<IRecordedType> recordedTypes, List<TypeToPatch> results)
        {
            foreach (Assembly currentAssembly in assemblies)
            {
                if (!IgnoredAssemblies.IsIgnoredAssembly(currentAssembly))
                {
                    SearchAssembly(recordedTypes, results, currentAssembly);
                }
            }
        }

        private static AssemblyDefinition GetAssemblyDefinition(Assembly assembly)
        {
            return AssemblyResolver.ReadAssembly(assembly);
        }

        private static void SearchAssembly(IReadOnlyCollection<IRecordedType> recordedTypes, List<TypeToPatch> ttr, Assembly currentAssembly)
        {
            AssemblyDefinition def = GetAssemblyDefinition(currentAssembly);
            if (def != null)
            {
                ParallelQuery<TypeToPatch> list =
                    currentAssembly.GetTypes().AsParallel()
                    .Select((t, r) => new TypeToPatch(t, TypeAnalyzer.FindInputSolutionsInType(def, recordedTypes, t)));
                ttr.AddRange(list);
            }
        }

        private static void TryAnalyzeProject(InputRecordingGlobalSettingsAsset settingsAsset, IReadOnlyCollection<IRecordedType> types)
        {
            try
            {
                settingsAsset.Clear();
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                FindReferencesForRecordedTypes(settingsAsset.settings, assemblies, types);
                AssemblyResolver.INSTANCE.Dispose();
                EditorUtility.SetDirty(settingsAsset);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error while trying to analyze project!");
                Debug.LogException(ex);
            }
        }
    }
}