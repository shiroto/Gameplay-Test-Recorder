using System.Reflection;

namespace TwoGuyGames.GTR.Core
{
    public interface IAssemblyVerifyer
    {
        bool IsAssemblyToSearch(Assembly assembly);
    }
}