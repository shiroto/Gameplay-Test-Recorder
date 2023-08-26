using UnityEngine;
using UnityEngine.InputSystem;

namespace TwoGuyGames.GTR.Samples
{
    public class RTSInput_InputSystem : MonoBehaviour, IRTSInput
    {
        private bool shouldMove;

        public Vector2 GetMovePosition()
        {
            return Mouse.current.position.ReadValue();
        }

        public bool ShouldMove()
        {
            return shouldMove;
        }

        private void LateUpdate()
        {
            shouldMove = false;
        }

        private void OnMove(InputValue movementValue)
        {
            shouldMove = true;
        }
    }
}