using System;
using TwoGuyGames.GTR.Core;

namespace TwoGuyGames.GTR.SystemRandomAdapter
{
    internal static class TypeInfo
    {
        public static string Key => Type.AssemblyQualifiedName;
        public static RecordedSystems RecordedSystems => RecordedSystems.SYSTEM_RANDOM;
        public static Type Type => typeof(System.Random);
    }
}