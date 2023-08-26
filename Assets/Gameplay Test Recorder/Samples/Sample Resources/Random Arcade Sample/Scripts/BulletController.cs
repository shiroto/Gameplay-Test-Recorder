using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject.Destroy(gameObject);
        }

        private void Start()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        }
    }
}