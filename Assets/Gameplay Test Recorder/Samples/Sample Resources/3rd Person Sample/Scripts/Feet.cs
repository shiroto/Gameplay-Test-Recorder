using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class Feet : MonoBehaviour
    {
        [SerializeField]
        private float groundDistance;

        [SerializeField]
        private bool isGrounded;

        public Vector3 GroundPosition
        {
            get;
            private set;
        }

        public bool IsGrounded
        {
            get
            {
                return isGrounded;
            }
        }

        public bool WasGrounded
        {
            get;
            private set;
        }

        private void FixedUpdate()
        {
            WasGrounded = isGrounded;
            isGrounded = Physics.SphereCast(
                ray: new Ray(transform.position, Vector3.down),
                radius: 0.5f,
                maxDistance: groundDistance,
                hitInfo: out RaycastHit hit);
            GroundPosition = hit.point;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDistance);
        }
    }
}