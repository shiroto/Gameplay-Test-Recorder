using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    [RequireComponent(typeof(CharacterController))]
    internal sealed class ThirdPersonCharacterControllerBase : MonoBehaviour
    {
        private CharacterController characterController;

        [SerializeField]
        private float fallAcceleration;

        [SerializeField]
        private Feet feet;

        private IThirdPersonInput input;

        [SerializeField]
        private float jumpForce;

        [SerializeField]
        private float maxFallSpeed;

        private Vector3 motion;

        private Vector3 moveInput;

        [SerializeField]
        private int points;

        private Vector3 rotation;

        [SerializeField]
        private float rotationSpeed;

        [SerializeField]
        private float runAcceleration;

        [SerializeField]
        private float runDeceleration;

        [SerializeField]
        private float speed;

        private void Fall()
        {
            if (!feet.IsGrounded)
            {
                moveInput.y -= fallAcceleration * Time.deltaTime;
                moveInput.y = Mathf.Max(moveInput.y, -maxFallSpeed);
            }
            if (!feet.WasGrounded && feet.IsGrounded)
            {
                moveInput.y = 0;
            }
        }

        private void MoveInput()
        {
            if (input.IsUp())
            {
                moveInput.z = Mathf.Lerp(moveInput.z, 1, runAcceleration * Time.deltaTime);
            }
            else if (input.IsDown())
            {
                moveInput.z = Mathf.Lerp(moveInput.z, -1, runAcceleration * Time.deltaTime);
            }
            else
            {
                moveInput.z = Mathf.Lerp(moveInput.z, 0, runDeceleration * Time.deltaTime);
            }
            if (input.IsLeft())
            {
                moveInput.x = Mathf.Lerp(moveInput.x, -1, runAcceleration * Time.deltaTime);
            }
            else if (input.IsRight())
            {
                moveInput.x = Mathf.Lerp(moveInput.x, 1, runAcceleration * Time.deltaTime);
            }
            else
            {
                moveInput.x = Mathf.Lerp(moveInput.x, 0, runDeceleration * Time.deltaTime);
            }
            if (input.IsJump())
            {
                moveInput.y = jumpForce;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Pickup"))
            {
                points++;
                GameObject.Destroy(other.gameObject);
            }
        }

        private void ProcessInput()
        {
            if (feet.IsGrounded)
            {
                MoveInput();
            }
            rotation.y = input.GetRotation();
        }

        private void Start()
        {
            input = GetComponent<IThirdPersonInput>();
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Fall();
            ProcessInput();
            if (feet.IsGrounded)
            {
                motion = transform.forward * moveInput.z + transform.right * moveInput.x;
            }
            characterController.Move(Vector3.up * moveInput.y * Time.deltaTime);
            characterController.Move(motion * speed * Time.deltaTime);
            transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
        }
    }
}