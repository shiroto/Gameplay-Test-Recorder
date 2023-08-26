using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class Bullet_TopDown : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        // Start is called before the first frame update
        private void Start()
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.tag.Equals("Player"))
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}