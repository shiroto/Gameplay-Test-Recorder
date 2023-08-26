using System;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    [RequireComponent(typeof(CharacterController))]
    internal sealed class TopDownCharacterControllerBase : MonoBehaviour
    {
        [SerializeField]
        private GameObject bulletPrefab;

        [SerializeField]
        private Transform muzzle;

        [SerializeField]
        private float rotationSpeed;

        [SerializeField]
        private float runDeceleration;

        [SerializeField]
        private float speed;

        private CharacterController characterController;

        private ITopDownInput input;

        private Vector3 motion;

        private Vector3 moveInput;

        private Vector3 rotation;

        public static event Action OnGameOver = delegate { };

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject.Destroy(gameObject);
                OnGameOver();
            }
        }

        private void MoveInput()
        {
            if (input.IsUp())
            {
                moveInput.z = input.GetZInput();
            }
            else if (input.IsDown())
            {
                moveInput.z = input.GetZInput();
            }
            else
            {
                moveInput.z = Mathf.Lerp(moveInput.z, 0, runDeceleration * Time.deltaTime);
            }
            if (input.IsLeft())
            {
                moveInput.x = input.GetXInput();
            }
            else if (input.IsRight())
            {
                moveInput.x = input.GetXInput();
            }
            else
            {
                moveInput.x = Mathf.Lerp(moveInput.x, 0, runDeceleration * Time.deltaTime);
            }
        }

        private void ProcessInput()
        {
            MoveInput();
            RotationInput();
        }

        private void RotationInput()
        {
            Vector3 direction = input.GetRotation();
            rotation = Vector3.right * direction.x + Vector3.forward * direction.y;
        }

        private void Start()
        {
            input = GetComponent<ITopDownInput>();
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            ProcessInput();
            motion = new Vector3(moveInput.x, 0, moveInput.z);
            characterController.Move(motion * speed * Time.deltaTime);
            if (rotation.sqrMagnitude > 0.0f)
            {
                Quaternion newRotation = Quaternion.LookRotation(rotation, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            }
            if (input.IsShooting())
            {
                GameObject.Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            }
        }
    }
}