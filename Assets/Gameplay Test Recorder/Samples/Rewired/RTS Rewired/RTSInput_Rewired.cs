#if REWIRED
using Rewired;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class RTSInput_Rewired : MonoBehaviour, IRTSInput
    {
        private Player player;

        public Vector2 GetMovePosition()
        {
            return ReInput.controllers.Mouse.screenPosition;
        }

        public bool ShouldMove()
        {
            return player.GetButton("Move");
        }

        private void Start()
        {
            player = ReInput.players.GetPlayer(0);
        }
    }
}
#endif