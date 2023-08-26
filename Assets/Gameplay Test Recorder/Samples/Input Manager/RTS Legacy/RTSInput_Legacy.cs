using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class RTSInput_Legacy : MonoBehaviour, IRTSInput
    {
        public Vector2 GetMovePosition()
        {
            return Input.mousePosition;
        }

        public bool ShouldMove()
        {
            return Input.GetMouseButtonDown(1);
        }
    }
}