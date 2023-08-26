using UnityEngine;

namespace TwoGuyGames.GTR.MousePositionReplayer
{
    public class Cursor : MonoBehaviour
    {
        public Transform cursorImage;
        public GameObject leftClickImage;

        public void SetLeftClick(bool b)
        {
            leftClickImage.SetActive(b);
        }

        public void SetPosition(Vector3 position)
        {
            cursorImage.position = position;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}