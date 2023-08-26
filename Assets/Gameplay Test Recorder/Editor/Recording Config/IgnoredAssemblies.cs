using UnityEngine.Assertions;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    /// <summary>
    /// Assemblies that should never be searched or changed.
    /// </summary>
    internal static class IgnoredAssemblies
    {
        private static List<IgnoredAssembly> ignoredAssemblies;

        static IgnoredAssemblies()
        {
            ignoredAssemblies = new List<IgnoredAssembly>
            {
                new IgnoredAssembly("mscorlib", Check.STARTS_WITH),
                new IgnoredAssembly("System", Check.STARTS_WITH),
                new IgnoredAssembly("UnityEditor", Check.STARTS_WITH),
                new IgnoredAssembly("Mono.Cecil", Check.STARTS_WITH),
                new IgnoredAssembly("Mono.Newtonsoft", Check.STARTS_WITH),
                new IgnoredAssembly("Harmony", Check.CONTAINS),
                new IgnoredAssembly("Rewired", Check.STARTS_WITH),
                new IgnoredAssembly("VisualStudio", Check.CONTAINS),
                new IgnoredAssembly("nunit", Check.STARTS_WITH),
                new IgnoredAssembly("ReportGeneratorMerged", Check.STARTS_WITH),
                new IgnoredAssembly("ExCSS.Unity", Check.STARTS_WITH),
                new IgnoredAssembly("Unity.VisualEffect", Check.STARTS_WITH),
                new IgnoredAssembly("Anonymously Hosted DynamicMethods Assembly", Check.STARTS_WITH),
                new IgnoredAssembly("TwoGuyGames.GTR", Check.CONTAINS),
            };
        }

        private enum Check
        {
            EQUALS,
            CONTAINS,
            STARTS_WITH
        }

        public static bool IsIgnoredAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                return true;
            }
            foreach (IgnoredAssembly ia in ignoredAssemblies)
            {
                if (ia.IsIgnoredAssembly(assembly))
                {
                    return true;
                }
            }
            return false;
        }

        private class IgnoredAssembly
        {
            public Check check;
            public string name;

            public IgnoredAssembly(string name, Check check)
            {
                this.check = check;
                this.name = name;
            }

            public bool IsIgnoredAssembly(Assembly assembly)
            {
                switch (check)
                {
                    case Check.EQUALS:
                        return assembly.FullName.StartsWith(name);

                    case Check.CONTAINS:
                        return assembly.FullName.Contains(name);

                    case Check.STARTS_WITH:
                        return assembly.FullName.StartsWith(name);

                    default:
                        throw new NotImplementedException(check + "");
                }
            }
        }
    }
}