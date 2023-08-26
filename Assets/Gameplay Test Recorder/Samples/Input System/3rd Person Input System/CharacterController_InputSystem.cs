using UnityEngine;
using UnityEngine.InputSystem;

namespace TwoGuyGames.GTR.Samples
{
    public class CharacterController_InputSystem : MonoBehaviour, IThirdPersonInput
    {
        private bool jump;
        private Vector3 moveInput;
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

        private void OnJump(InputValue inputValue)
        {
            jump = true;
        }

        private void OnLook(InputValue inputValue)
        {
            Vector2 look = inputValue.Get<Vector2>();
            rotation = look.x;
        }

        private void OnMove(InputValue movementValue)
        {
            Vector2 input = movementValue.Get<Vector2>();
            moveInput.x = input.x;
            moveInput.z = input.y;
        }
    }
};