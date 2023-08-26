using UnityEngine;
using UnityEngine.Events;

namespace TwoGuyGames.GTR.Samples
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField]
        private GameObject bulletPrefab;

        [SerializeField]
        private Transform muzzle;

        private Rigidbody2D myRigidbody;

        [SerializeField]
        private UnityEvent<Vector3> onMoveInput;

        [SerializeField]
        private float speed;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector3 input = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                input += Vector3.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                input += Vector3.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                input += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                input += Vector3.right;
            }
            myRigidbody.AddForce(input * speed * Time.fixedDeltaTime);
            onMoveInput.Invoke(input);
        }

        private void OnDisable()
        {
            GameOver.OnGameOver -= OnGameOver;
        }

        private void OnEnable()
        {
            GameOver.OnGameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
            }
        }
    }
}