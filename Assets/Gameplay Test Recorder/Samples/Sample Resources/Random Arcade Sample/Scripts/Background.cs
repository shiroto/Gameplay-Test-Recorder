using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class Background : MonoBehaviour
    {
        [SerializeField]
        private float scrollSpeed;

        private void Update()
        {
            transform.position += Vector3.left * Time.deltaTime * scrollSpeed;
            if (transform.position.x < -21)
            {
                Vector3 pos = transform.position;
                pos.x += 60;
                transform.position = pos;
            }
        }
    }
}