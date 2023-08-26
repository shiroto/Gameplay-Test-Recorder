#if REWIRED
using System;
using TwoGuyGames.GTR.Core;

namespace TwoGuyGames.GTR.RewiredAdapter
{
    internal static class TypeInfo
    {
        public static string Key => Type.AssemblyQualifiedName;
        public static RecordedSystems RecordedSystems => RecordedSystems.REWIRED;
        public static Type Type => typeof(Rewired.Player);
        public static Type TypeMouse => typeof(Rewired.Mouse);
    }
}
#endif