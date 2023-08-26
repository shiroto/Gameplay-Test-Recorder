using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    internal sealed class CharacterController_Legacy : MonoBehaviour, IThirdPersonInput
    {
        public float GetRotation()
        {
            return Input.GetAxis("Mouse X");
        }

        public bool IsDown()
        {
            return Input.GetKey(KeyCode.S);
        }

        public bool IsJump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool IsLeft()
        {
            return Input.GetKey(KeyCode.A);
        }

        public bool IsRight()
        {
            return Input.GetKey(KeyCode.D);
        }

        public bool IsUp()
        {
            return Input.GetKey(KeyCode.W);
        }
    }
};