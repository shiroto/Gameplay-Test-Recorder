using System;
using TwoGuyGames.GTR.Core;

namespace TwoGuyGames.GTR.UnityRandomAdapter
{
    internal static class TypeInfo
    {
        public static string Key => Type.AssemblyQualifiedName;
        public static RecordedSystems RecordedSystems => RecordedSystems.UNITY_RANDOM;
        public static Type Type => typeof(UnityEngine.Random);
    }
}