using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private float closedY;

        [SerializeField]
        private float openSpeed;

        [SerializeField]
        private float openY;

        public void Open()
        {
            StartCoroutine(DoOpen());
        }

        private IEnumerator DoOpen()
        {
            while (transform.position.y > openY)
            {
                Vector3 pos = transform.position;
                pos.y = Mathf.Lerp(transform.position.y, openY, openSpeed);
                transform.position = pos;
                yield return null;
            }
            SetOpen();
        }

        private void SetOpen()
        {
            Vector3 pos = transform.position;
            pos.y = openY;
            transform.position = pos;
        }
    }
}