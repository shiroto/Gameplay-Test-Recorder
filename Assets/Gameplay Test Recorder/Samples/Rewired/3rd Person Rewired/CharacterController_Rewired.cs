#if REWIRED
using Rewired;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public sealed class CharacterController_Rewired : MonoBehaviour, IThirdPersonInput
    {
        private bool jump;
        private Vector3 moveInput;
        private Player player;
        private float rotation;

        public float GetRotation()
        {
            return rotation;
        }

        public bool IsDown()
        {
            return moveInput.z < 0;
        }

        public bool IsJump()
        {
            return jump;
        }

        public bool IsLeft()
        {
            return moveInput.x < 0;
        }

        public bool IsRight()
        {
            return moveInput.x > 0;
        }

        public bool IsUp()
        {
            return moveInput.z > 0;
        }

        private void LateUpdate()
        {
            jump = false;
        }

        private void Start()
        {
            player = ReInput.players.GetPlayer(0);
        }

        private void Update()
        {
            moveInput.x = player.GetAxis("Move Horizontal");
            moveInput.z = player.GetAxis("Move Vertical");
            rotation = player.GetAxis("Mouse X");
            jump = player.GetButton("Jump");
        }
    }
}
#endif