namespace TwoGuyGames.GTR.Core
{
    public class ReplayResultHelper
    {
        /// <summary>
        /// >100: big difference (very likely an error)
        /// </summary>
        public const float BIG_DIFFERENCE = 100;

        /// <summary>
        /// 10-100 medium difference (maybe little error)
        /// </summary>
        public const float MEDIUM_DIFFERENCE = 10;

        /// <summary>
        /// 0-1: no difference (no error)
        /// </summary>
        public const float NO_DIFFERENCE = 0;

        /// <summary>
        /// 1-10: small difference (likely no error)
        /// </summary>
        public const float SMALL_DIFFERENCE = 1;
    }
}