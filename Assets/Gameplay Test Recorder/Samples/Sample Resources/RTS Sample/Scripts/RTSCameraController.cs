using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    [ExecuteAlways]
    public class RTSCameraController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 cameraOffset;

        [SerializeField]
        private float lerpSpeed;

        private Transform target;

        private void Start()
        {
            target = GameObject.FindObjectOfType<RTSPlayerController>().transform;
        }

        private void Update()
        {
            if (target != null)
            {
                transform.position = Vector3.Lerp(transform.position, target.transform.position + cameraOffset, Time.deltaTime * lerpSpeed);
                transform.LookAt(transform);
            }
        }
    }
}