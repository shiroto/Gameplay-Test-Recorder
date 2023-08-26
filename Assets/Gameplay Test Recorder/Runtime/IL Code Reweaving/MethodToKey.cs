using System.Reflection;

namespace TwoGuyGames.GTR.Core
{
    public static class MethodToKey
    {
        public static string GetKey(MethodBase mi)
        {
            return $"{mi.DeclaringType.Name}.{mi.Name}";
        }
    }
}