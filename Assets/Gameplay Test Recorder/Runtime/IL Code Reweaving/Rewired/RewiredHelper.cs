using System;
using System.Linq;
using System.Reflection;

namespace TwoGuyGames.GTR.Core
{
    internal static class RewiredHelper
    {
        static RewiredHelper()
        {
            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                Assembly rewired = assemblies.First(a => a.FullName.Contains("Rewired_Core"));
                InputType = rewired.GetType("Rewired.Player");
            }
            catch (Exception)
            {
                InputType = null;
            }
        }

        public static Type InputType
        {
            get;
            private set;
        }
    }
}