using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    internal interface IRTSInput
    {
        Vector2 GetMovePosition();

        bool ShouldMove();
    }
}