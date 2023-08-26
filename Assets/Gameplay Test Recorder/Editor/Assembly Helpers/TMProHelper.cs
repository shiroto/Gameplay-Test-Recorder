using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.Editor
{
    internal static class TMProHelper
    {
        static TMProHelper()
        {
            TryGetTMPro();
        }

        public static IReadOnlyList<TypeToPatch> TypesToPatch
        {
            get;
            private set;
        }

        private static void TryGetTMPro()
        {
            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                Assembly assembly = assemblies.First(a => a.FullName.Equals("Unity.TextMeshPro, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"));

                List<TypeToPatch> ttr = new List<TypeToPatch>();

                Type inputField = assembly.GetType("TMPro.TMP_InputField");
                FieldInfo field = inputField.GetField("m_ProcessingEvent", BindingFlags.NonPublic | BindingFlags.Instance);
                ttr.Add(new TypeToPatch(inputField, new[] { field }, new[] { new StaticMock(typeof(Event)) }));

                TypesToPatch = ttr;
            }
            catch (Exception)
            {
                // TMPro doesn't exist in this project.
                TypesToPatch = new TypeToPatch[0];
            }
        }
    }
}