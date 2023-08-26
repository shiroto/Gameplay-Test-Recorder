namespace TwoGuyGames.GTR.Core
{
    public static class ArgHelper
    {
        public static bool IsBatchmode()
        {
            return GetArg("-batchmode") != null;
        }

        public static bool IsReplayEnabled()
        {
            return GetArg("-noReplays") == null;
        }

        private static string GetArg(string name)
        {
            var args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == name)
                {
                    return name;
                }
            }
            return null;
        }
    }
}