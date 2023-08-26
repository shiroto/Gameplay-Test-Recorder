using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class TopThrusterVisuals : MonoBehaviour
    {
        public void OnMoveInput(Vector3 input)
        {
            if (input.y < 0)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}