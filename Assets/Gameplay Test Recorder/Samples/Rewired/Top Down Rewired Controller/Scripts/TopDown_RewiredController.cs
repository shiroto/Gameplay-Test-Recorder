#if REWIRED
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class TopDown_RewiredController : MonoBehaviour, ITopDownInput
    {
        private bool shoot;
        private Vector3 moveInput;
        private Player player;
        private Vector3 rotation;

        public Vector3 GetRotation()
        {
            return rotation;
        }

        public bool IsDown()
        {
            return moveInput.z < 0;
        }

        public bool IsLeft()
        {
            return moveInput.x < 0;
        }

        public bool IsRight()
        {
            return moveInput.x > 0;
        }

        public bool IsUp()
        {
            return moveInput.z > 0;
        }

        public float GetXInput()
        {
            return moveInput.x;
        }

        public float GetZInput()
        {
            return moveInput.z;
        }

        public bool IsShooting()
        {
            return shoot;
        }

        private void Start()
        {
            player = ReInput.players.GetPlayer(0);
        }

        private void Update()
        {
            moveInput.x = player.GetAxis("Move Horizontal");
            moveInput.z = player.GetAxis("Move Vertical");
            rotation.x = player.GetAxis("AimX");
            rotation.y = player.GetAxis("AimY");
            shoot = player.GetButtonDown("Shoot");
        }
    }
}
#endif