using TwoGuyGames.GTR.Core;
using UnityEngine;

namespace TwoGuyGames.GTR.MousePositionReplayer
{
    public class MouseReplayer : IReplayer
    {
        private Cursor cursor;
        public string Key => "MOUSE";

        public void FixedUpdate(ReplayEventArgs args)
        {
        }

        public void Init(ReplayEventArgs args)
        {
        }

        public void LateUpdate(ReplayEventArgs args)
        {
        }

        public void StartReplaying(ReplayEventArgs args)
        {
            GameObject cursorPrefab = Resources.Load<GameObject>("Cursor Canvas");
            GameObject go = GameObject.Instantiate(cursorPrefab);
            go.hideFlags = HideFlags.HideInHierarchy;
            cursor = go.GetComponent<Cursor>();
        }

        public void StopReplaying(ReplayEventArgs args)
        {
        }

        public void Update(ReplayEventArgs args)
        {
            cursor.SetPosition(ValueRecorder.NextInput<Vector3>(Key));
            cursor.SetLeftClick(ValueRecorder.NextInput<bool>(Key));
        }
    }
}